app.service('ujet.messaging', ['$window', function ($window) {
    function addEventListener(win, event, callback) {
        if (win.addEventListener)
            win.addEventListener(event, function(e) { callback(e); }, false);

        else if (win.attachEvent)
            win.attachEvent('on' + event, function(e) { callback(e); });
    }

    function onLoad(e, callback) {
        callback();
    }

    function onMessage(e, callback) {
        if (e.origin !== $window.location.origin)
            return;

        callback(e.data);
    }

    var service = {
        load: function(win, callback) {
            addEventListener(win, 'load', function(e) { onLoad(e, callback); });
        },

        sendMessage: function(win, message) {
            win.postMessage(message, $window.location.origin);
        },

        getMessages: function(win, callback) {
            addEventListener(win, 'message', function(e) { onMessage(e, callback); });
        }
    };

    return service;
}]);