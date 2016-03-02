(function () {
    "use strict";

    app.factory("ujetLeafletFactory", ujetLeafletFactory);

    ujetLeafletFactory.$inject = ["$window"];

    function ujetLeafletFactory($window) {
        return $window.L;
    };
})();