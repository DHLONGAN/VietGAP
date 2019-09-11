
function ShapesMap() {

    // state
    
    var _newShapeNextId = 0;
    var _shapes = Array();
    // types

    var RECTANGLE = google.maps.drawing.OverlayType.RECTANGLE;
    var CIRCLE = google.maps.drawing.OverlayType.CIRCLE;
    var POLYGON = google.maps.drawing.OverlayType.POLYGON;
    var POLYLINE = google.maps.drawing.OverlayType.POLYLINE;
    var MARKER = google.maps.drawing.OverlayType.MARKER;

    function typeDesc(type) {
        switch (type) {
            case RECTANGLE:
                return "rectangle";

            case CIRCLE:
                return "circle";

            case POLYGON:
                return "polygon";

            case POLYLINE:
                return "polyline";

            case MARKER:
                return "marker";

            case null:
                return "null";

            default:
                return "UNKNOWN GOOGLE MAPS OVERLAY TYPE";
        }
    }

    // json reading

    function jsonReadPath(jsonPath) {
        var path = new google.maps.MVCArray();

        for (var i = 0; i < jsonPath.path.length; i++) {
            var latlon =
                new google.maps.LatLng(jsonPath.path[i].lat, jsonPath.path[i].lon);
            path.push(latlon);
        }

        return path;
    }

    function jsonReadRectangle(jsonRectangle) {
        var jr = jsonRectangle;
        var southWest = new google.maps.LatLng(
            jr.bounds.southWest.lat,
            jr.bounds.southWest.lon);
        var northEast = new google.maps.LatLng(
            jr.bounds.northEast.lat,
            jr.bounds.northEast.lon);
        var bounds = new google.maps.LatLngBounds(southWest, northEast);

        var rectangleOptions = {
            bounds: bounds,
            strokeWeight: 0,
            editable: false,
            fillColor: jr.color,
            map: map
        };

        var rectangle = new google.maps.Rectangle(rectangleOptions);

        return rectangle;
    }

    function jsonReadCircle(jsonCircle) {
        var jc = jsonCircle;

        var center = new google.maps.LatLng(
            jc.center.lat,
            jc.center.lon);

        var circleOptions = {
            center: center,
            radius: parseFloat(jc.radius),
            strokeWeight: 0,
            editable: false,
            fillColor: jc.color,
            map: map
        };

        var circle = new google.maps.Circle(circleOptions);

        return circle;
    }

    function jsonReadPolyline(jsonPolyline) {
        var path = jsonReadPath(jsonPolyline);

        var polylineOptions = {
            path: path,
            editable: false,
            strokeColor: jsonPolyline.color,
            map: map
        };

        var polyline = new google.maps.Polyline(polylineOptions);

        return polyline;
    }

    function jsonReadPolygon(jsonPolygon) {
        var paths = new google.maps.MVCArray();

        for (var i = 0; i < jsonPolygon.paths.length; i++) {
            var path = jsonReadPath(jsonPolygon.paths[i]);
            paths.push(path);
        }

        var polygonOptions = {
            paths: paths,
            strokeWeight: 0,
            editable: false,
            fillColor: jsonPolygon.color,
            map: map
        };

        var polygon = new google.maps.Polygon(polygonOptions);

        return polygon;
    }

    function jsonRead(json) {
        var jsonObject = eval("(" + json + ")");

        for (i = 0; i < jsonObject.shapes.length; i++) {
            switch (jsonObject.shapes[i].type) {
                case RECTANGLE:
                    var rectangle = jsonReadRectangle(jsonObject.shapes[i]);
                    newShapeSetProperties(rectangle, RECTANGLE);
                    newShapeAddListeners(rectangle);
                    shapesAdd(rectangle);
                    break;

                case CIRCLE:
                    var circle = jsonReadCircle(jsonObject.shapes[i]);
                    newShapeSetProperties(circle, CIRCLE);
                    newShapeAddListeners(circle);
                    shapesAdd(circle);
                    break;

                case POLYLINE:
                    var polyline = jsonReadPolyline(jsonObject.shapes[i]);
                    newShapeSetProperties(polyline, POLYLINE);
                    newShapeAddListeners(polyline);
                    shapesAdd(polyline);
                    break;

                case POLYGON:
                    var polygon = jsonReadPolygon(jsonObject.shapes[i]);
                    newShapeSetProperties(polygon, POLYGON);
                    newShapeAddListeners(polygon);
                    shapesAdd(polygon);
                    break;
            }
        }
    }  

    // storage

    function shapesAdd(shape) {
        _shapes.push(shape);
    }
    function shapesLoad() {
        var start_length = _shapes.length;
        var cookies = latlngs.split(";");
        for (var i = 0; i < cookies.length; i++) {
            var key = cookies[i].substr(0, cookies[i].indexOf("="));
            key = key.replace("/^\s+|\s+$/g", "");

            if (key == "shapes") {
                var value = cookies[i].substr(cookies[i].indexOf("=") + 1);
                jsonRead(value);
            }
        }

    }

    function newShapeAddListeners(shape) {
        google.maps.event.addListener(
            shape,
            'click',
            function () {
                //onShapeClicked(shape);
                alert(shape.type);
            });

        switch (shape.type) {
        }
    }

    function newShapeSetProperties(shape, type) {
        shape.type = type;
        shape.appId = _newShapeNextId;
        _newShapeNextId++;
    }

    // map creation
  
    shapesLoad();
}
