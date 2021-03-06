﻿var clients = [];
$(function () {
    clients = $.clientsInit();
})
$.clientsInit = function () {
    var dataJson = {
        dataItems: [],
        organize: [],
        role: [],
        group:[],
        duty: [],
        user: [],
        authorizeMenu: [],
        authorizeButton: []
    };
    var init = function () {
        $.ajax({
            url: "../ClientsData/GetClientsDataJson",
            type: "get",
            dataType: "json",
            async: false,
            success: function (data) {
                //console.log(data);
                dataJson.dataItems = data.dataItems;
                dataJson.organize = data.organize;
                dataJson.role = data.role;
                dataJson.group = data.group;
                dataJson.duty = data.duty;
                dataJson.authorizeMenu = eval(data.authorizeMenu);
                dataJson.authorizeButton = data.authorizeButton;
            }
        });
    }
    init();
    return dataJson;
}