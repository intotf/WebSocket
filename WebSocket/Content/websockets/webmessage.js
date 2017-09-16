// websocket的flash文件，给老IE用的
WEB_SOCKET_SWF_LOCATION = "/Content/websockets/WebSocketMain.swf";
WEB_SOCKET_DEBUG = false;

var ws;

function initWs(userId) {
    var address = "ws://10.0.3.88:1850";//location.protocol.replace('http', 'ws') + '//' + location.host + ":1850";
    ws = new jsonWebSocket(address);
    ws.onopen = function (e) {
        ws.invokeApi("bindUser", [userId]);
    };

    ws.bindApi("onWebNotify", function (title, message) {
        $.get('/Home/WebMessage', { title: title, message: message }).done(function (html) {
            $("#webmessage").html(html);
        });

        var backMsg = ["我已经收到的数据=" + message];
        ws.invokeApi("SendMessage", backMsg, function (data) {
            $("#backMsg").append("返回服务端："+backMsg[0] + "<br>");
        });

        if (message) {
            toastr.clear();
            toastr.options.closeButton = true;
            toastr.error(message, title, { timeOut: 10 * 1000 });
        }
    });
}

