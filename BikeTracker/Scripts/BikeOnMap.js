var Sii = Sii || {};

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

Sii.findPositionOnRoute = function (distance, leg) {
    if (distance >= leg.distance.value) {
        return leg.end_location;
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