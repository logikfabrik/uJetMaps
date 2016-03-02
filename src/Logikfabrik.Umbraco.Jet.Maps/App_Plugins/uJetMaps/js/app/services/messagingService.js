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
         * Adds an event listener for the load event and wires it to call the callback 
         * function when the load event is raised.
         * 
         * @param {} win The window to add the event listener to.
         * @param {} callback The function to call when the load event is raised.
         * @returns {undefined} 
         */
        function onLoad(win, callback) {
            addEventListener(win, "load", function() { callback(); });
        }

        /**
         * Sends a message.
         * 
         * @param {} win The window to send the message through.
         * @param {} message The message to send.
         * @returns {undefined} 
         */
        function sendMessage(win, message) {
            win.postMessage(message, $window.location.origin);
        }

        /**
         * Adds an event listener for the message event and wires it to call the callback 
         * function when a message event is raised.
         * 
         * @param {} win The window to add the event listener to.
         * @param {} callback The function to call when the message event is raised.
         * @returns {undefined} 
         */
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