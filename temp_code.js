$(document).on("click", "#btnSavePolygons", function () {   
    map.removeLayer(fgroup);

    var inputObj = {};

    var geo = {};
    var testPolygon = []; 

    fgroup.eachLayer(function (layer) {
        if (layer.toGeoJSON().type == "Feature") {
            console.log("Feature");
            console.log(layer.toGeoJSON()); 
            testPolygon.push(layer.toGeoJSON().geometry.coordinates);
        } 
        //layerlardaki coordinatları togeojson ıle alacaksın
    });

    if (testPolygon.length == 1) {
        geo.type = "Polygon";
        geo.coordinates = testPolygon;
        inputObj.GeoPoly = geo;
    }
    else {
        geo.type = "MultiPolygon";
        geo.coordinates = testPolygon;
        inputObj.GeoMultipoly = geo;
    }

    App.Requests.SavePolygon(inputObj); 
});
















SavePolygon: function (sendedObject) {
    var r = xhrs["xhrSavePolygon"];
    if (r && r.readyState != 4) {
        r.abort();
    }
    r = App.Ajax.Post("v1/score/SavePolygon", sendedObject)
        .done(function (data) {
            alert(data);
        })
        .fail(function (e) {
            console.log("servis hatası", e)
        });
    dbdenGelenPoligon.clearLayers();
},




























var App = (function (t) {

    function AuthRequiredAction(jqXHR, textStatus, errorThrown) {
 
        if (t.Ajax.authRequiredCallback != null && typeof (t.Ajax.authRequiredCallback) == 'function') {
            t.Ajax.authRequiredCallback();
        }
    }

    var baseUrl = "";

    t.Ajax = {

        callbackHeader : null,
        authRequiredCallback : null,
        doneCallback : null,
        errorCallback : null,
        completeCallback: null,

        getBaseUrl : function () {
            return baseUrl;
        },

        setBaseUrl : function (val) {
            baseUrl = val;
        },

        Get : function (url, data) {
            var settings = {};
            settings.type = 'GET';
            settings.url = baseUrl + url;
            settings.contentType = 'application/json';
            settings.data = data;
            settings.dataType = 'json';
            settings.statusCode = {
                401: function () {
                    AuthRequiredAction();
                }
            };

            if (t.Ajax.callbackHeader != null && typeof (t.Ajax.callbackHeader) == 'function') {
                var r = t.Ajax.callbackHeader();
                settings.headers = r;
            }
            return jQuery.ajax(settings).done(t.Ajax.doneCallback).fail(t.Ajax.errorCallback).always(t.Ajax.completeCallback);
        },

        Post : function (url, data) {

            var settings = {};
            settings.type = 'POST';
            settings.url = baseUrl + url;
            //settings.headers = { 'Access-Control-Allow-Origin': 'http://localhost'};
            settings.contentType = 'application/json';
            settings.data = JSON.stringify(data);
            settings.dataType = 'json';
            settings.processData = true;
            settings.statusCode = {
                401: function () {
                    AuthRequiredAction();
                }
            };

            if (t.Ajax.callbackHeader != null && typeof (t.Ajax.callbackHeader) == 'function') {
                var r = t.Ajax.callbackHeader();
                settings.headers = r;
            }

            return jQuery.ajax(settings).done(t.Ajax.doneCallback).fail(t.Ajax.errorCallback).always(t.Ajax.completeCallback);
        }

    }
    return t;

}(App || {}));