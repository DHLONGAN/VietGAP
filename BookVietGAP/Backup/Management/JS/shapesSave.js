﻿
function ShapesMap(_mapContainer, _deleteButton, _mylocation) {

    // state
    var _clearButton = null;

    var _selection = null;
    var _map = null;
    var _drawingManager = null;
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
            //fillColor: selectColor,
            map: _map
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
            //fillColor: selectColor,
            map: _map
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
            //strokeColor: selectColor,
            map: _map
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
            //fillColor: selectColor,
            map: _map
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

    // json writing

    function comma(i) {
        return (i > 0) ? ',' : '';
    }

    function jsonMakeLatlon(latlon) {
        var buf =
            '"lat":"' + latlon.lat() + '","lon":"' + latlon.lng() + '"';

        return buf;
    }

    function jsonMakeBounds(bounds) {
        var buf =
            '"bounds":{'
            + '"northEast":{' + jsonMakeLatlon(bounds.getNorthEast()) + '},'
            + '"southWest":{' + jsonMakeLatlon(bounds.getSouthWest()) + '}'
            + '}';

        return buf;
    }

    function jsonMakeType(type) {
        var buf = '"type":"' + typeDesc(type) + '"';

        return buf;
    }

    function jsonMakeColor(color) {
        //var buf = '"color":"' + color + '"';
        var buf = '"color":"' + selectColor + '"';
        return buf;
    }

    function jsonMakeCenter(center) {
        var buf = '"center":{' + jsonMakeLatlon(center) + '}';

        return buf;
    }

    function jsonMakeRadius(radius) {
        var buf = '"radius":"' + radius + '"';

        return buf;
    }

    function jsonMakePath(path) {
        var n = path.getLength();

        var buf = '"path":[';
        for (var i = 0; i < n; i++) {
            var latlon = path.getAt(i);

            buf += comma(i) + '{' + jsonMakeLatlon(latlon) + '}';
        }
        buf += ']';

        return buf;
    }

    function jsonMakePaths(paths) {
        var n = paths.getLength();

        var buf = '"paths":[';
        for (var i = 0; i < n; i++) {
            var path = paths.getAt(i);

            buf += comma(i) + '{' + jsonMakePath(path) + '}';
        }
        buf += ']';

        return buf;
    }

    function jsonMakeRectangle(rectangle) {
        var buf =
            jsonMakeType(RECTANGLE) + ','
            //+ jsonMakeColor(rectangle.fillColor) + ','
            + jsonMakeColor(selectColor) + ','        
            + jsonMakeBounds(rectangle.bounds);

        return buf;
    }

    function jsonMakeCircle(circle) {
        var buf =
            jsonMakeType(CIRCLE) + ','
            + jsonMakeColor(circle.fillColor) + ','
            + jsonMakeCenter(circle.center) + ','
            + jsonMakeRadius(circle.radius);

        return buf;
    }

    function jsonMakePolyline(polyline) {
        var buf =
            jsonMakeType(POLYLINE) + ','
            //+ jsonMakeColor(polyline.strokeColor) + ','
            + jsonMakeColor(selectColor) + ','
            + jsonMakePath(polyline.getPath());

        return buf;
    }

    function jsonMakePolygon(polygon) {
        var buf =
            jsonMakeType(POLYGON) + ','
            + jsonMakeColor(selectColor) + ','
            //+ jsonMakeColor(polygon.fillColor) + ','
            + jsonMakePaths(polygon.getPaths());

        return buf;
    }

    function jsonMake() {
        var buf ="";
        for (i = 0; i < _shapes.length; i++) {
            switch (_shapes[i].type) {
                case RECTANGLE:
                    buf += comma(i) + '{' + jsonMakeRectangle(_shapes[i]) + '}';
                    break;

                case CIRCLE:
                    buf += comma(i) + '{' + jsonMakeCircle(_shapes[i]) + '}';
                    break;

                case POLYLINE:
                    buf += comma(i) + '{' + jsonMakePolyline(_shapes[i]) + '}';
                    break;

                case POLYGON:
                    buf += comma(i) + '{' + jsonMakePolygon(_shapes[i]) + '}';
                    break;
            }
        }

        return buf;
    }

    // storage

    function shapesAdd(shape) {
        _shapes.push(shape);
    }

    function shapesDelete(shape) {
        var found = false;

        for (var i = 0; i < _shapes.length && !found; i++) {
            if (_shapes[i] === shape) {
                _shapes.splice(i, 1);
                found = true;
            }
        }
    }

    function shapesHideAll() {
        for (var i = 0; i < _shapes.length; i++) {
            _shapes[i].setMap(null);
        }
    }

    function shapesDeleteAll() {
        print(_shapes.length + " shapes deleted\n");

        _shapes.splice(0, _shapes.length);
    }

    function shapesSave() {
        var shapes = jsonMake();
//        Addtxt(shapes);
        nlat = shapes;
        
    }

    function shapesLoad() {
       
            if (latlngs != "") {
                jsonRead(latlngs);
            }
    }

    // printing

    function print(string) {
        //        _console.innerHTML += string;
        //        _console.scrollTop = _console.scrollHeight;
    }

    function printDrawingMode(drawingManager) {
        print(
            "drawing mode set to "
            + typeDesc(drawingManager.getDrawingMode())
            + "\n");
    }

    // selection

    function selectionPrint() {
        if (_selection == null) {
            print("selection cleared\n");
        }
        else {
            print(_selection.appId + ": selected\n");
        }
    }

    function selectionIsSet() {
        return _selection != null;
    }

    function selectionSet(newSelection) {
        if (newSelection == _selection) {
            return;
        }

        if (_selection != null) {
            _selection.setEditable(false);
            _selection = null;
        }

        if (newSelection != null) {
            _selection = newSelection;
            _selection.setEditable(true);
        }

        selectionPrint();
    }

    function selectionClear() {
        selectionSet(null);
    }

    function selectionDelete() {
        if (_selection != null) {
            _selection.setMap(null);
            selectionClear();
        }
    }

    // new shape integration

    function newShapeAddPathListeners(shape, path) {
        google.maps.event.addListener(
            path,
            'insert_at',
            function () { onShapeEdited(shape) });
        google.maps.event.addListener(
            path,
            'remove_at',
            function () { onShapeEdited(shape) });
        google.maps.event.addListener(
            path,
            'set_at',
            function () { onShapeEdited(shape) });
    }

    function newShapeAddListeners(shape) {
        google.maps.event.addListener(
            shape,
            'click',
            function () { onShapeClicked(shape); });

        switch (shape.type) {
            case RECTANGLE:
                google.maps.event.addListener(
                shape,
                'bounds_changed',
                function () { onShapeEdited(shape); });
                break;

            case CIRCLE:
                google.maps.event.addListener(
                shape,
                'center_changed',
                function () { onShapeEdited(shape); });
                google.maps.event.addListener(
                shape,
                'radius_changed',
                function () { onShapeEdited(shape); });
                break;

            case POLYLINE:
                var path = shape.getPath();
                newShapeAddPathListeners(shape, path);
                break;

            case POLYGON:
                var paths = shape.getPaths();

                var n = paths.getLength();
                for (var i = 0; i < n; i++) {
                    var path = paths.getAt(i);
                    newShapeAddPathListeners(shape, path);
                }
                break;
        }
    }

    function newShapeSetProperties(shape, type) {
        shape.type = type;
        shape.appId = _newShapeNextId;

        _newShapeNextId++;
    }

    // map creation

    function createMap(mapContainer) {
        //var center = new google.maps.LatLng(42.3583, -71.0603);

        var mapOptions = {
            zoom: zooms,
            center: center,
            mapTypeId: google.maps.MapTypeId.HYBRID,
            panControl: false,
            mapTypeControl: true,
            mapTypeControlOptions: {
                style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR,
                position: google.maps.ControlPosition.TOP_RIGHT
            },
            scaleControl: false,
            streetViewControl: false,
            disableDefaultUI: false,
            zoomControl: true,
            zoomControlOptions: {
                style: google.maps.ZoomControlStyle.LARGE,
                position: google.maps.ControlPosition.RIGHT_CENTER
            },
            setMyLocationEnabled: true
        };

        map = new google.maps.Map(mapContainer, mapOptions);
        
        google.maps.event.addListener(map, 'click', onMapClicked);

        return map;
    }
    function Mylocation() {
        GeoMarker = new GeolocationMarker();
        GeoMarker.setCircleOptions({ fillColor: '#808080' });

        google.maps.event.addListenerOnce(GeoMarker, 'position_changed', function () {
            map.setCenter(this.getPosition());
            map.fitBounds(this.getBounds());
        });

        google.maps.event.addListener(GeoMarker, 'geolocation_error', function (e) {
            alert('There was an error obtaining your position. Message: ' + e.message);
        });

        GeoMarker.setMap(map);
    }
    
    // drawing manager creation

    function drawingManagerCreate() {

        // create drawing manager

        var drawingModes = new Array(POLYGON);

        var drawingControlOptions = {
            drawingModes: drawingModes,
            position: google.maps.ControlPosition.BOTTOM_RIGHT
        };

        var polyOptions = {
            strokeWeight: 0,
            editable: true,
            fillColor: selectColor,
            strokeWeight: 3
        };

        drawingManagerOptions = {
            drawingMode: null,
            drawingControlOptions: drawingControlOptions,
            markerOptions: { draggable: true },
            polylineOptions: { editable: true },
            drawingControl: drawingCon,
            rectangleOptions: polyOptions,
            circleOptions: polyOptions,
            polygonOptions: polyOptions,
            map: _map
        };

        drawingManager = new google.maps.drawing.DrawingManager(
            drawingManagerOptions);

        // tie events to map

        google.maps.event.addListener(
            drawingManager,
            'overlaycomplete',
            onNewShape);
        google.maps.event.addListener(
            drawingManager,
            'drawingmode_changed',
            onDrawingModeChanged);

        // print initial drawing mode, selection

        printDrawingMode(drawingManager);
        selectionPrint();

        return drawingManager;
    }

    // event capture

    function onNewShape(event) {
        var shape = event.overlay;

        newShapeSetProperties(shape, event.type);
        newShapeAddListeners(shape);
        shapesAdd(shape);
        shapesSave();
        selectionSet(shape);

        print("new " + typeDesc(event.type) + " created (id = "
              + shape.appId + ")\n");
    }

    function onShapeEdited(shape) {
        print(shape.appId + ": shape edited\n");
        shapesSave();
    }

    function onShapeClicked(shape) {
        print(shape.appId + ": shape clicked\n");
        selectionSet(shape);
    }

    function onMapClicked() {
        print("map clicked\n");
        selectionClear();
    }

    function onDeleteButtonClicked() {
        print("delete button clicked\n");

        if (selectionIsSet()) {
            shapesDelete(_selection);
            shapesSave();
            selectionDelete();
        }
    }

    function onClearButtonClicked() {
        print("clear button clicked\n");

        selectionClear();
        shapesHideAll();
        shapesDeleteAll();
        shapesSave();
    }

    function onDrawingModeChanged() {
        printDrawingMode(drawingManager);
        selectionClear();
    }

    function onCreate() {
        _map = createMap(_mapContainer);
        _drawingManager = drawingManagerCreate(_map);

        google.maps.event.addDomListener(
            _deleteButton,
            'click',
            onDeleteButtonClicked);
        google.maps.event.addDomListener(
            _mylocation,
            'click',
            Mylocation);
//        google.maps.event.addDomListener(
//            _clearButton,
//            'click',
//            onClearButtonClicked);

        shapesLoad();
    }

    // initialization

    onCreate();
}
