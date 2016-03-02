(function () {
    "use strict";

    app.controller("ujetEditorController", ujetEditorController);

    ujetEditorController.$inject = ["$scope", "$element", "$window", "$q", "localizationService", "ujetMessagingService"];

    function ujetEditorController($scope, $element, $window, $q, localizationService, ujetMessagingService) {
        $scope.model.config.localization = {
            title: null,
            placeholder: null
        };

        $q.all([
                localizationService.localize("uJet_mapsTitle")
                .then(function(value) {
                    $scope.model.config.localization.title = value;
                }),
                localizationService.localize("uJet_mapsPlaceholder")
                .then(function(value) {
                    $scope.model.config.localization.placeholder = value;
                })
            ])
            .then(function() {
                var frameWin = $element[0];

                // Send the model to the frame once the frame has loaded.
                ujetMessagingService.onLoad(frameWin, function() {
                    ujetMessagingService.sendMessage(frameWin.contentWindow, $scope.model);
                });

                // Get the model from the frame, and update the value.
                ujetMessagingService.onMessage($window, function(data) {
                    if (data.alias !== $scope.model.alias) {
                        return;
                    }

                    $scope.model.value = data.value;
                });
            });
    };
})();