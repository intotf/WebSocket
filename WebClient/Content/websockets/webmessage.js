// websocket的flash文件，给老IE用的
WEB_SOCKET_SWF_LOCATION = "/Content/websockets/WebSocketMain.swf";
WEB_SOCKET_DEBUG = false;
var ws;
//用户登录
function UserLogin() {
    userid = $("#id").val();
    userName = $("#name").val();
    var serverIp = "10.0.3.88:1850";
    var address = "ws://" + serverIp; //location.protocol.replace('http', 'ws') + '//' + location.host + ":1850";
    ws = new jsonWebSocket(address);

    ws.onclose = function (e) {
        layer.alert(serverIp + "<br>服务器连接失败.");
    }

    ws.onopen = function (e) {
        console.log(e);
        if (!ws.connected) {
            layer.alert(serverIp + "<br>服务器连接失败.");
        }
        ws.invokeApi("UserLogin", [userid, userName], function (data) {
            if (!data.State) {
                alert('登录失败');
            } else {
                $("#userInfo div,#userInfo button").remove();
                $("#userInfo").append("<label class=\"btn btn-success\">" + userName + "</label>");
                $("#userName").val(userName);
                $("#userId").val(userid);
                $("#sendButton").removeAttr("disabled");
            }
        });

        //接收服务端发送过来的信息
        // message = 消息内容；sender 发送者；time 发送时间
        ws.bindApi("onWebNotify", function (data) {
            var msg = "<br/> <b>" + data.sender + "</b> [" + data.SendTime + "]对你说:" + data.Content + "\n";
            $("#txtMsg").append(msg);
        });
    };

    //更新在线人数
    ws.bindApi("setOnlien", function (data) {
        $("#userlist").empty().append('<li><a href="javascript:void(0)" onclick="onAddTag(\'0\', \'所有人员\')">@所有人员</a></li>');
        $.each(data, function (i, n) {
            $("#userlist").append('<li><a href="javascript:void(0)" onclick="onAddTag(\'' + n.id + '\', \'' + n.name + '\')">' + n.name + '</a></li>');
        })
    })
}

//发送消息给指定人员
function SendMessageToUsers(users, msg) {
    ws.invokeApi("SendMessageToUsers", [users, msg], function (data) {
        console.log(data);
    });
}