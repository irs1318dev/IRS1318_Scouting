// ****************************************************************** //
// The following implemenation of OfflineUtility uses polling to determine if 
// there is an active connection to the web. There are a few conventions
// that are overrideable if you choose. If you do not pass in a 
// value for 'pollingUrl' then the script expects a file named 'o.html'
// to be at the root of your application.
//
// While this approach for determining if an internet connection enjoys
// cross-browser support, there is still no guarantee the current browser
// supports application cache APIs - so keep that in mind if you plan 
// on using this in conjunction with HTML offline apps :)
// ****************************************************************** //
//
//var OfflineUtility = function (onlineCallback, offlineCallback, pollingUrl) {
//    // **************************************************************
//    // *** The following can be removed in production contexts ******
//    // **************************************************************
//    var DEBUG_MODE = true;
//    // **************************************************************

//    if (!pollingUrl) {
//        pollingUrl = '/o.html';
//    }

//    var
//        currentEventName = 'unknown',
//        debugCheckBoxId = '___forceOffline',

//        fireEvent = function (name, data) {
//            var e = document.createEvent("Event");
//            e.initEvent(name, true, true);
//            e.data = data;
//            window.dispatchEvent(e);
//        },

//        fireEventIfStatusChanges = function (eventName) {
//            if (currentEventName != eventName) {
//                currentEventName = eventName;
//                fireEvent(eventName, {});
//            }
//        },

//        getUrl = function () {
//            // **************************************************************
//            // *** The following can be removed in production contexts ******
//            // **************************************************************
//            if (DEBUG_MODE) {
//                if ($('#' + debugCheckBoxId).is(':checked')) {
//                    return '14cfb6f6-415c-40d8-997f-35aeb2163dfd.html';
//                }
//            }
//            // **************************************************************
//            return pollingUrl;
//        },

//        // approach via @paul_kinlan
//        // http://www.html5rocks.com/en/mobile/workingoffthegrid/
//        detectOnlineStatus = function (url) {
//            $.get(url).done(function () {
//                fireEventIfStatusChanges('onlineCustom');
//            }).fail(function () {
//                fireEventIfStatusChanges('offlineCustomer');
//            });
//        },

//        autoReloadOnCacheUpdate = function () {
//            window.applicationCache.swapCache();
//            location.reload();
//        };

//    // **************************************************************
//    // *** The following can be removed in production contexts ******
//    // **************************************************************
//    if (DEBUG_MODE) {
//        var
//            container = $('<div></div>'),
//            checkBox = $('<input type="checkbox" width="auto" />'),
//            label = $('<label>Simulate Offline</label>');

//        checkBox.attr('id', debugCheckBoxId);
//        checkBox.attr('name', debugCheckBoxId);

//        label.attr('for', debugCheckBoxId);

//        container.attr('style', 'position:fixed;left:4px;bottom:4px;background:yellow;');
//        container.append(checkBox);
//        container.append(label);

//        $('body').append(container);
//    }
//    // **************************************************************

//    if (window.applicationCache) {
//        $(window.applicationCache).on('updateready', autoReloadOnCacheUpdate);
//    }

//    if (onlineCallback) {
//        window.addEventListener("onlineCustom", onlineCallback);
//    }

//    if (offlineCallback) {
//        window.addEventListener("offlineCustom", offlineCallback);
//    }

//    if (onlineCallback || offlineCallback) {
//        detectOnlineStatus();
//        setInterval(function () {
//            var url = getUrl();
//            detectOnlineStatus(url);
//        }, 3000);
//    }
//};


// ****************************************************************** //
// The following version of OfflineUtility uses the native browser 
// implementaiton of online detection using the window.online
// and window.offline events in conjunction with the navigator.onLine
// property. This approach is effective, but not supported by 
// all browers in the same way. For instance FireFox does not raise
// the online/offline events when connectivity is lost from the browser.
// If this is not an issue for you then this version of OfflineUtility
// may be a good choice for your application.
// ****************************************************************** //
var OfflineUtility = function(onlineCallback, offlineCallback) {
    var
        autoReloadOnCacheUpdate = function() {
            window.applicationCache.swapCache();
            location.reload();
        };

    if (onlineCallback) {
        $(window).on('online', onlineCallback);
    }

    if (offlineCallback) {
        $(window).on('offline', offlineCallback);
    }

    if (window.applicationCache) {
        $(window.applicationCache).on('updateready', autoReloadOnCacheUpdate);
    }

    if (navigator.onLine) {
        onlineCallback();
    } else {
        offlineCallback();
    }
};
//