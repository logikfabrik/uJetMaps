app.controller('ujet.editor', ['$scope', '$element', '$window', '$q', 'localizationService', 'ujet.messaging',
    function ($scope, $element, $window, $q, localizationService, messaging) {
        $scope.model.config.localization = {
            title: null,
            placeholder: null
        };

        var await = $q.all([
            localizationService.localize('uJet_mapsTitle').then(function (value) {
                $scope.model.config.localization.title = value;
            }),
            localizationService.localize('uJet_mapsPlaceholder').then(function (value) {
                $scope.model.config.localization.placeholder = value;
            })
        ]);

        await.then(function () {
            var map = $element[0];

            // Send model message.
            messaging.load(map, function () {
                messaging.sendMessage(map.contentWindow, $scope.model);
            });

            // Get model messages.
            messaging.getMessages($window, function (data) {
                if (data.alias !== $scope.model.alias)
                    return;

                $scope.model.value = data.value;
            });
        });
    }]);