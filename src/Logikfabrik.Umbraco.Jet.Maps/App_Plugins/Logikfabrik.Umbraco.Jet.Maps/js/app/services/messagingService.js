(function() {
    "use strict";

    app.service("ujetMessagingService", ujetMessagingService);

    ujetMessagingService.$inject = ["$window"];

    
    function ujetMessagingService($window) {
        var service = {
            onLoad: onLoad,
            sendMessage: sendMessage,
            onMessage: onMessage
        };

        return service;

        function addEventListener(win, event, callback) {
            if (win.addEventListener) {
                win.addEventListener(event, function (e) { callback(e); }, false);

            } else if (win.attachEvent) {
                win.attachEvent("on" + event, function (e) { callback(e); });

            }
        }

        /**
         * 
         * @param {} win The window.
         * @param {} callback The function to call when the load event is raised.
         * @returns {} 
         */
        function onLoad(win, callback) {
            addEventListener(win, "load", function() { callback(); });
        }

        function sendMessage(win, message) {
            win.postMessage(message, $window.location.origin);
        }

        function onMessage(win, callback) {
            addEventListener(win, "message", function(e) {
                if (e.origin !== $window.location.origin) {
                    return;
                }

                callback(e.data);
            });
        }
    };
})();