(function () {
    "use strict";

    app.controller("ujetMapController", ujetMapController);

    ujetMapController.$inject = ["$scope", "$window", "ujetLeafletFactory", "ujetMessagingService"];

    // ReSharper disable once InconsistentNaming
    function ujetMapController($scope, $window, L, ujetMessagingService) {
        var map;
        var layer;
        var model = {};

        /**
         * Determines whether the specified coordinates are valid.
         * 
         * @param {Object} latlng The coordinates to validate.
         * @returns {boolean} True if valid, otherwise; false.
         */
        function isLatlng(latlng) {
            if (latlng == null) {
                return false;
            }

            return latlng.hasOwnProperty("lat") && latlng.hasOwnProperty("lng");
        };

        /**
         * Draws a marker on the map at the specified coordinates.
         * 
         * @param {Object} latlng The coordinates to draw the marker at.
         * @param {boolean} moveTo Whether to center the view.
         * @returns {undefined} 
         */
        function drawMapMarker(latlng, moveTo) {
            model.value = latlng;
            ujetMessagingService.sendMessage($window.parent, model);

            layer.clearLayers();

            if (!isLatlng(latlng)) {
                return;
            }

            var ll = L.latLng(latlng.lat, latlng.lng);

            layer.addLayer(L.marker(ll)
                .on("click", function () {
                    drawMapMarker(null, false);
                }));

            if (moveTo) {
                map.setView(ll, 12);
            }
        };

        /**
         * Draws the map.
         * 
         * @returns {undefined} 
         */
        function drawMap() {
            map = L.map("map")
                .on("click", function (e) {
                    drawMapMarker(e.latlng, false);
                });

            map.setMaxBounds(L.latLngBounds(L.latLng(-90, -180), L.latLng(90, 180)));
            map.setView([0, 0], 1);

            L.esri.basemapLayer("Topographic")
                .addTo(map);

            layer = new L.LayerGroup()
                .addTo(map);

            new L.esri.Geocoding.Controls.Geosearch({
                zoomToResult: false,
                useMapBounds: false,
                allowMultipleResults: false,
                placeholder: model.config.localization.placeholder,
                title: model.config.localization.title
            })
                .addTo(map)
                .on("results", function (data) {
                    if (data.results.length === 0) {
                        return;
                    }

                    drawMapMarker(data.results[0].latlng, true);
                });
        };

        ujetMessagingService.onMessage($window, function (data) {
            model = data;

            drawMap();
            drawMapMarker(model.value, true);
        });
    };
})();