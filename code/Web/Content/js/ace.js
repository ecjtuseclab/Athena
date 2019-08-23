
/* 异步请求结果返回状态码 */
var ResultStatus = { OK: 100, Failed: 101, NotLogin: 102, Unauthorized: 103 };

Date.prototype.format = function (format) {
    var o =
        {
            "M+": this.getMonth() + 1, //month
            "d+": this.getDate(),    //day
            "H+": this.getHours(),   //hour
            "m+": this.getMinutes(), //minute
            "s+": this.getSeconds(), //second
            "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
            "S": this.getMilliseconds() //millisecond
        };
    if (/(y+)/.test(format))
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(format))
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
    return format;
};
Array.prototype.where = function (predicate) {
    var arr = this;
    var retArr = [];
    for (var i = 0; i < arr.length; i++) {
        var item = arr[i];
        if (predicate(item)) {
            retArr.push(item);
        }
    }
    return retArr;
};
Array.prototype.select = function (selector) {
    var arr = this;
    var retArr = [];
    for (var i = 0; i < arr.length; i++) {
        var item = arr[i];
        retArr.push(selector(item));
    }
    return retArr;
};

/* $ace */
(function ($) {
    var $ace = {};

    /* 返回 json 数据 */
    $ace.get = function (url, data, callback) {
        if ($.isFunction(data)) {
            callback = data;
            data = undefined;
        }

        url = parseUrl(url);
        var ret = execAjax("GET", url, data, callback);
        return ret;
    }
    $ace.post = function (url, data, callback) {
        if ($.isFunction(data)) {
            callback = data;
            data = undefined;
        }

        var ret = execAjax("POST", url, data, callback);
        return ret;
    }

    $ace.alert = function (msg, callBack) {
        layerAlert(msg, callBack);
    }
    $ace.confirm = function (msg, callBack) {
        layerConfirm(msg, callBack);
    }
    $ace.msg = function (msg) {
        layerMsg(msg);
    }

    $ace.reload = function () {
        location.reload();
        return false;
    }

    /* 将当前 url 的参数值 */
    $ace.getQueryParam = function (name) {
        if (name === null || name === undefined || name === "")
            return "";
        name = ("" + name).toLowerCase();
        var search = location.search.slice(1);
        var arr = search.split("&");
        for (var i = 0; i < arr.length; i++) {
            var ar = arr[i].split("=");
            if (ar[0].toLowerCase() == name) {
                if (unescape(ar[1]) == 'undefined') {
                    return "";
                } else {
                    return unescape(ar[1]);
                }
            }
        }
        return "";
    }
    /*获取Content缩略字符*/
    $ace.getFuzzyContent = function (content) {
        if (content == "" || content == undefined || content == null) {
            return content;
        }
        return (content.length > 10 ? content.substring(0, 9) + '....' : content);


    }
    /* 将当前 url 参数转成一个 js 对象 */
    $ace.getQueryParams = function () {
        var params = {};
        var loc = window.location;
        var se = decodeURIComponent(loc.search);

        if (!se) {
            return params;
        }

        var paramsString;
        paramsString = se.substr(1);//将?去掉
        var arr = paramsString.split("&");
        for (var i = 0; i < arr.length; i++) {
            var item = arr[i];
            var index = item.indexOf("=");
            if (index == -1)
                continue;
            var paramName = item.substr(0, index);
            if (!paramName)
                continue;
            var value = item.substr(index + 1);
            params[paramName] = value;
        }
        return params;
    }

    /* optionList: [{"Value" : "1", "Text" : "开发部"},{"Value" : "2", "Text" : "测试部"}] */
    $ace.getOptionTextByValue = function (optionList, value, valuePropName, textPropName)
    {
        valuePropName = valuePropName || "Value";
        textPropName = textPropName || "Text";

        var text = "";
        var len = optionList.length;
        for (var i = 0; i < len; i++) {
            if (optionList[i][valuePropName] == value) {
                text = optionList[i][textPropName];
                break;
            }
        }

        return text;
    }

    /* 依赖 bootstrap ui */
    $ace.selectRow = function (selectedTr) {
        var c = "warning";
        $(selectedTr).addClass(c);
        $(selectedTr).siblings().removeClass(c);
        return true;
    }
    $ace.formatBool = function (val) {
        if (val == true) {
            return "是";
        }
        else if (val == false) {
            return "否";
        }

        return val;
    }


    function execAjax(type, url, data, callback) {
        var layerIndex = layer.load(1);
        var ret = $.ajax({
            url: url,
            type: type,
            dataType: "json",
            data: data,
            complete: function (xhr) {
                layer.close(layerIndex);
            },
            success: function (result) {
                var isStandardResult = ("Status" in result) && ("Msg" in result);
                if (isStandardResult) {
                    if (result.Status == ResultStatus.Failed) {
                        layerAlert(result.Msg || "操作失败");
                        return;
                    }
                    if (result.Status == ResultStatus.NotLogin) {
                        layerAlert(result.Msg || "未登录或登录过期，请重新登录");
                        return;
                    }
                    if (result.Status == ResultStatus.Unauthorized) {
                        layerAlert(result.Msg || "权限不足，禁止访问");
                        return;
                    }

                    if (result.Status == ResultStatus.OK) {
                        /* 传 result，用 result.Data 还是 result.Msg，由调用者决定 */
                        callback(result);
                    }
                }
                else
                    callback(result);
            },
            error: errorCallback
        });
        return ret;
    }
    function errorCallback(xhr, textStatus, errorThrown) {
        var json = { textStatus: textStatus, errorThrown: errorThrown };
        alert("请求失败: " + JSON.stringify(json));
    }
    function parseUrl(url) {
        if (url.indexOf("_dc=") < 0) {
            if (url.indexOf("?") >= 0) {
                url = url + "&_dc=" + (new Date().getTime());
            } else {
                url = url + "?_dc=" + (new Date().getTime());
            }
        }
        return url;
    };

    function layerAlert(msg, callBack) {
        msg = msg == null ? "" : msg;/* layer.alert 传 null 会报错 */
        var type = 'warning';
        var icon = "";
        if (type == 'success') {
            icon = "fa-check-circle";
        }
        if (type == 'error') {
            icon = "fa-times-circle";
        }
        if (type == 'warning') {
            icon = "fa-exclamation-circle";
        }

        var idx;
        idx = layer.alert(msg, {
            icon: icon,
            title: "系统提示",
            btn: ['确认'],
            btnclass: ['btn btn-primary'],
        }, function () {
            layer.close(idx);
            if (callBack)
                callBack();
        });
    }
    function layerConfirm(content, callBack) {
        var idx;
        idx = layer.confirm(content, {
            icon: "fa-exclamation-circle",
            title: "系统提示",
            btn: ['确认', '取消'],
            btnclass: ['btn btn-primary', 'btn btn-danger'],
        }, function () {
            layer.close(idx);
            callBack();
        }, function () {

        });
    }
    function layerMsg(msg, callBack) {
        msg = msg == null ? "" : msg;/* layer.msg 传 null 会报错 */
        layer.msg(msg, { time: 2000, shift: 0 });
    }


    window.$ace = $ace;
})($);


/* $ 扩展 */
(function ($) {

    /* 设置 input 标签的值，同时触发 change 事件 */
    $.fn.setValue = function (val) {
        this.val(val);
        this.change();
    }

    /* 根据值设置 select 标签的选中项，同时触发 change 事件 */
    $.fn.setSelectedValue = function (val) {
        for (var j = 0; j < this.length; j++) {
            var selectElement = this[j];
            for (var i = 0; i < selectElement.options.length; i++) {
                var op = selectElement.options[i];
                if (op.value == val) {
                    op.selected = true;
                    $(selectElement).change();
                    break;
                }
            }
        }
    };
    $.fn.getSelectedValue = function () {
        var ret = "";
        var c = "";
        for (var j = 0; j < this.length; j++) {
            var selectElement = this[j];
            var idx = selectElement.selectedIndex;
            ret += c + selectElement[idx].value;
            c = ";";
        }

        return ret;
    };
    $.fn.getSelectedText = function () {
        var ret = "";
        var c = "";
        for (var j = 0; j < this.length; j++) {
            var selectElement = this[j];
            var idx = selectElement.selectedIndex;
            ret += c + selectElement[idx].text;
            c = ";";
        }

        return ret;
    };

    $.fn.formValid = function () {
        return $(this).valid({
            errorPlacement: function (error, element) {
                element.parents('.formValue').addClass('has-error');
                element.parents('.has-error').find('i.error').remove();
                element.parents('.has-error').append('<i class="form-control-feedback fa fa-exclamation-circle error" data-placement="left" data-toggle="tooltip" title="' + error + '"></i>');
                $("[data-toggle='tooltip']").tooltip();
                if (element.parents('.input-group').hasClass('input-group')) {
                    element.parents('.has-error').find('i.error').css('right', '33px')
                }
            },
            success: function (element) {
                element.parents('.has-error').find('i.error').remove();
                element.parent().removeClass('has-error');
            }
        });
    }

})($);



function ViewModelBase() {
    var me = this;

    me.SearchModel = _ob({});
    me.DeleteUrl = null;

    /* 如有必要，子类需重写 DataTable、Dialog */
    me.DataTable = new PagedDataTable(me);
    me.Dialog = new DialogBase();

    //me.Detail = function () {
    //    EnsureNotNull(me.DataTable, "DataTable");
    //    EnsureNotNull(me.Dialog, "Dialog");
    //    me.Dialog.Open(me.DataTable.SelectedModel(), "详情");
    //}

    me.Add = function () {
        EnsureNotNull(me.Dialog, "Dialog");
        me.Dialog.Open(null, "添加");
    }
    me.Edit = function () {
        EnsureNotNull(me.DataTable, "DataTable");
        EnsureNotNull(me.Dialog, "Dialog");
        me.Dialog.Open(me.DataTable.SelectedModel(), "修改");
    }
    me.Delete = function () {
        $ace.confirm("确定要删除该条数据吗？", me.OnDelete);
    }

    me.OnDelete = function () {
        DeleteRow();
    }
    /* 要求每行必须有 Id 属性 */
    function DeleteRow() {
        if (me.DeleteUrl == null)
            throw new Error("未指定 DeleteUrl");

        var url = me.DeleteUrl;
        var params = { id: me.DataTable.SelectedModel().id };
        $ace.post(url, params, function (result) {
            var msg = result.Msg || "删除成功";
            $ace.msg(msg);
            me.DataTable.RemoveSelectedModel();
        });
    }

    me.Search = function () {
        me.LoadModels();
    }

    me.LoadModels = function () {
        throw new Error("未重写 LoadModels 方法");
    }

    function EnsureNotNull(obj, name) {
        if (!obj)
            throw new Error("属性 " + name + " 未初始化");
    }
}
function DataTableBase() {
    var me = this;

    me.Models = _oba([]);
    me.SelectedModel = _ob(null);

    me.GetOrdinal = function ($index) {
        return $index + 1;
    }
    me.SelectRow = function (model, event) {
        me.SelectedModel(model);
        $ace.selectRow(event.currentTarget);
        return true;
    }
    me.RemoveSelectedModel = function () {
        var selectedModel = me.SelectedModel();
        if (selectedModel) {
            me.Models.remove(selectedModel);
        }
    }

    me.SetModels = function (models) {
        var wrapedModels = $ko.toOb(models);
        me.Models(wrapedModels);
    }
}
function PagedDataTable(vmOrLoadModelsFn) {
    var me = this;
    DataTableBase.call(me);

    me.ShowFirstPage = _ob(false);
    me.ShowLastPage = _ob(false);
    me.TotalCount = _ob(0);
    me.TotalPage = _ob(0);
    me.CurrentPage = _ob(0);
    me.SkipPage = _ob(0);
    me.PageSize = _ob(0);

    me.ShowPages = _oba();

    me.GetOrdinal = function ($index) {
        return (me.CurrentPage() - 1) * me.PageSize() + $index + 1;
    }
                     
    me.SetPagedData = function (pagedData) {
        var wrapedData = $ko.toOb(pagedData);
        me.Models(wrapedData.DataList());
        me.TotalCount(wrapedData.TotalCount());
        me.TotalPage(wrapedData.TotalPage());
        me.CurrentPage(wrapedData.CurrentPage());
        me.SkipPage(wrapedData.CurrentPage());
        me.PageSize(wrapedData.PageSize());

        var showPageCount = 6;

        var min = me.CurrentPage() - (showPageCount / 2);

        if (min < 1)
            min = 1;

        var max = min + showPageCount;

        if (max > me.TotalPage()) {
            max = me.TotalPage();
            min = max - showPageCount;
            if (min < 1)
                min = 1;
        }

        var showPages = [];
        for (var i = min; i <= max; i++) {
            showPages.push(i);
        }
        me.ShowPages(showPages);

        me.ShowFirstPage(min != 1);
        me.ShowLastPage(max != me.TotalPage());
    }
    me.ToPage = function (page) {
        if (page < 1 || page > me.TotalPage() || page == me.CurrentPage())
            return;
        me.LoadModels(page);
    }

    me.LoadModels = function (page) {
        if (!vmOrLoadModelsFn)
            throw new Error("未实现 LoadModels 方法");

        if (isFunction(vmOrLoadModelsFn)) {
            vmOrLoadModelsFn(page);
        }
        else {
            if ("LoadModels" in vmOrLoadModelsFn)
                vmOrLoadModelsFn.LoadModels(page);
            else
                throw new Error("vmOrLoadModelsFn 未实现 LoadModels 方法");
        }
    }

    function isFunction(obj) {
        return typeof obj === "function";
    }
}
function DialogBase() {
    var me = this;

    /* must */
    me.Title = _ob(null);
    me.IsShow = _ob(false);
    //“保存”按钮显隐性
    me.ShowSaveButton = _ob(true);

    me.EditModel = _ob(null);
    me.Model = _ob({});//绑定到界面的 model
    /* end must */

    /* must */
    me.Close = function () {
        me.IsShow(false);
    }
    me.Open = function (model, title) {
        
        me.IsShow(true);
        me.Model({});
        if (model) {
            me.EditModel(model);
        }
        else {
            me.EditModel(null);
        }

        if (title)
            me.Title(title);

        me.OnOpen();
    }
    me.Save = function () {
        me.OnSave();
    }
    /* end must */


    me.OnOpen = function () {
       //  me.ShowSaveButton(false);
        var model = me.EditModel();
        if (model) {
            var bindModel = $ko.toJS(model);
            me.Model(bindModel);
        }
        else {
            me.Model({});
        }
    }
    me.OnSave = function () {
        throw new Error("未重写 OnSave 方法");
    }
}
