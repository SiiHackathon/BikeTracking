var Sii = Sii || {};


Sii.loadRoute = function (map, url, teamsUrl, callback) {
	$.ajax({
		url: url,
		success: function (data) {
			callback(map, data, teamsUrl);
		}
	})
};

Sii.displayRoute = function (map, route, teamsUrl) {
	var request = {
		origin: route.Origin,
		destination: route.Destination,
		travelMode: 'BICYCLING'
	};
	var directionsService = new google.maps.DirectionsService();
	var directionsDisplay = new google.maps.DirectionsRenderer();
	directionsDisplay.setMap(map);
	directionsService.route(request, function (result, status) {
		if (status == 'OK') {
			directionsDisplay.setDirections(result);
			Sii.route = result.routes[0].legs[0];
			Sii.loadTeams(map, teamsUrl);
		}
	});
};

Sii.loadTeams = function (map, teamsUrl) {
	$.ajax({
		url: teamsUrl,
		success: function (data) {
			$.each(data, function (index, team) {
				Sii.addTeamMarker(map, team);
			});
			Sii.createTeamTable();
		}
	});
};

Sii.addTeamMarker = function (map, team) {

	var content = '<div id="iw-container"><div class="iw-title">' +
		team.Name +
		'</div><div class="iw-content"><div class="iw-subTitle">Covered distance</div><img src="' +
		team.Image.replace("~", "") +
		'" style="max-width:100px"><p>'
		+ team.CurrentDistance / 1000 +
		'km</p><div class="iw-subTitle">Members</div><ul>';
	$.each(team.Users, function (index, value) {
		content += "<li>" + value.Name + "</li>";
	});
	content += '</ul></div><div class="iw-bottom-gradient"></div></div>';;


	var point = Sii.findPositionOnRoute(team.TeamId, team.CurrentDistance, Sii.route, team.ReverseRoute);

	var marker = new google.maps.Marker({
		position: point,
		map: map,
		title: team.Name,
		icon: team.ReverseRoute ? '/Images/GoogleMaps/bicycle-pin-64-from.png' : '/Images/GoogleMaps/bicycle-pin-64-to.png',
		animation: google.maps.Animation.DROP,
	});
	
	marker.infowindow = new google.maps.InfoWindow({
		content: content,
		maxWidth: 350
	});

	google.maps.event.addListener(marker.infowindow, 'domready', function () {

		// Reference to the DIV that wraps the bottom of infowindow
		var iwOuter = $('.gm-style-iw');
		var iwOuter = $('.gm-style-iw').attr('class', function (i, s) { return s + ' bike-marker' });
		iwOuter.children(':nth-child(1)').attr('style', function (i, s) { return s + 'width: 100% !important;' });

		var iwBackground = iwOuter.prev();

		// Removes background shadow DIV
		iwBackground.children(':nth-child(2)').css({ 'display': 'none' });

		// Removes white background DIV
		iwBackground.children(':nth-child(4)').css({ 'display': 'none' });

		// Moves the infowindow 115px to the right.
		//iwOuter.parent().parent().css({ left: '115px' });

		// Moves the shadow of the arrow 76px to the left margin.
		iwBackground.children(':nth-child(1)').attr('style', function (i, s) { return s + 'left: 76px !important;' });

		// Moves the arrow 76px to the left margin.
		iwBackground.children(':nth-child(3)').attr('style', function (i, s) { return s + 'left: 76px !important;' });

		// Changes the desired tail shadow color.
		iwBackground.children(':nth-child(3)').find('div').children().css({ 'box-shadow': 'rgba(0, 32, 96, 0.6) 0px 1px 6px', 'z-index': '1', 'border-width': '1px', 'border-style': 'solid', 'border-color': 'transparent #002060 transparent #002060' });

		// Reference to the div that groups the close button elements.
		var iwCloseBtn = iwOuter.next();

		// Apply the desired effect to the close button
		iwCloseBtn.css({ opacity: '1', right: '60px', top: '25px', 'border-radius': '13px', 'box-shadow': '#002060 0px 0px 0px 6px' });

	});

	google.maps.event.addListener(marker, 'click', function () {
		map.panTo(marker.position);
		marker.infowindow.open(map, marker);
	});

	// Event that closes the Info Window with a click on the map
	google.maps.event.addListener(map, 'click', function () {
		marker.infowindow.close();
	});

};

Sii.createTeamTable = function () {
	$.ajax({
		type: 'GET',
		url: '/Team/TeamStandings?distance=' + Sii.route.distance.value,
		context: $('#users-scores-table'),
		cache: false,
		global: false,
		complete: function () {
		},
		success: function (result) {
			this.html(result);
			$('#standings-table').DataTable({
				pageLength: 5,
				order: [[1, "desc"], [2, "desc"]],
				searching: false,
				language: {
					paginate: {
						first: "|<",
						last: ">|",
						next: ">",
						previous: "<"
					}
				},
			});
		}
	});
}

Sii.getDistance = function (start, end) {
	var lat1 = start.lat();
	var lon1 = start.lng();
	var lat2 = end.lat();
	var lon2 = end.lng();
	var R = 6371e3;
	var f1 = lat1 * Math.PI / 180;
	var f2 = lat2 * Math.PI / 180;
	var df = (lat2 - lat1) * Math.PI / 180;
	var dt = (lon2 - lon1) * Math.PI / 180;

	var a = Math.sin(df / 2) * Math.sin(df / 2) +
            Math.cos(f1) * Math.cos(f2) *
            Math.sin(dt / 2) * Math.sin(dt / 2);
	var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));

	return R * c;
};

Sii.findPositionOnRoute = function (teamId, distance, leg, isReverse) {
	if (distance >= leg.distance.value) {
	    distance = distance - leg.distance.value;
		$.ajax({
			url: '/Ajax/TrackCompleted',
			type: "POST",
			data: {
				teamId: teamId,
				distance: distance
			},
			dataType: 'json'
		});
		return Sii.findPositionOnRoute(teamId, distance, leg, !isReverse);
	}
	if (isReverse) {
	    distance = leg.distance.value - distance;
	}
	var i = 0;
	var currentDistance = 0;
	while (i < leg.steps.length) {
		if (distance < currentDistance + leg.steps[i].distance.value) {
			break;
		} else if (distance == currentDistance + leg.steps[i].distance.value) {
			return leg.steps[i].end_location;
		}
		currentDistance += leg.steps[i].distance.value;
		i++;
	}
	var j = -1;
	var delta = 0;
	while (j < leg.steps[i].path.length - 1) {
		j++;
		delta = Math.round(Sii.getDistance(leg.steps[i].path[j], leg.steps[i].path[j + 1]));
		if (distance < currentDistance + delta) {
			break;
		} else if (distance == currentDistance + delta) {
			return leg.steps[i].path[j + 1];
		}
		currentDistance += delta;
	}
	var percentage = (distance - currentDistance) / delta;
	var lat = leg.steps[i].path[j].lat() + (leg.steps[i].path[j + 1].lat() - leg.steps[i].path[j].lat()) * percentage;
	var lng = leg.steps[i].path[j].lng() + (leg.steps[i].path[j + 1].lng() - leg.steps[i].path[j].lng()) * percentage;
	return new google.maps.LatLng(lat, lng);
};