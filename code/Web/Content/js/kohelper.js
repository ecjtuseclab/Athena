/*
* ko.bindingHandlers.display 依赖 bootstrap 模态框组件
*/

(function () {
    if (!ko)
        return;

    window._ob = ko.observable;
    window._oba = ko.observableArray;

    function format(format) {
        var o =
            {
                "M+": this.getMonth() + 1, //month
                "d+": this.getDate(),    //day
                "h+": this.getHours(),   //hour
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
    }

    function parseISO8601Date(s) {

        // parenthese matches:
        // year month day    hours minutes seconds  
        // dotmilliseconds 
        // tzstring plusminus hours minutes
        //var re = /(\d{4})-(\d\d)-(\d\d)T(\d\d):(\d\d):(\d\d)(\.\d+)?(Z|([+-])(\d\d):(\d\d))/;
        var re = /(\d{4})-(\d\d?)-(\d\d?)\s?T?(\d\d?)?:?(\d\d?)?:?(\d\d?)?(\.\d+)?(Z|([+-])(\d\d):(\d\d))?/;

        var d = [];
        d = s.match(re);

        // "2010-12-07T11:00:00.000-09:00" parses to:
        //  ["2010-12-07T11:00:00.000-09:00", "2010", "12", "07", "11",
        //     "00", "00", ".000", "-09:00", "-", "09", "00"]
        // "2010-12-07T11:00:00.000Z" parses to:
        //  ["2010-12-07T11:00:00.000Z",      "2010", "12", "07", "11", 
        //     "00", "00", ".000", "Z", undefined, undefined, undefined]

        if (!d) {
            //throw "Couldn't parse ISO 8601 date string '" + s + "'";
            return new Date(s);
        }

        // parse strings, leading zeros into proper ints
        var a = [1, 2, 3, 4, 5, 6, 10, 11];
        for (var i in a) {
            d[a[i]] = parseInt(d[a[i]], 10);
            if (!d[a[i]]) d[a[i]] = 0;
        }
        d[7] = parseFloat(d[7]);

        // Date.UTC(year, month[, date[, hrs[, min[, sec[, ms]]]]])
        // note that month is 0-11, not 1-12
        // see https://developer.mozilla.org/en/JavaScript/Reference/Global_Objects/Date/UTC
        var ms = Date.UTC(d[1], d[2] - 1, d[3], d[4], d[5], d[6]);

        // if there are milliseconds, add them
        if (d[7] > 0) {
            ms += Math.round(d[7] * 1000);
        }

        // if there's a timezone, calculate it
        if (d[8] != "Z" && d[10]) {
            var offset = d[10] * 60 * 60 * 1000;
            if (d[11]) {
                offset += d[11] * 60 * 1000;
            }
            if (d[9] == "-") {
                //ms -= offset;
                ms += offset;
            } else {
                //ms += offset;
                ms -= offset;
            }
        } else {
            ms -= 8 * 60 * 60 * 1000; // 因为之前用UTC算的时间,现在要减回来
        }

        return new Date(ms);
    }

    function registerEventHandler(element, eventType, handler) {
        if (typeof jQuery != "undefined") {
            jQuery(element)['bind'](eventType, handler);
        } else if (typeof element.addEventListener == "function")
            element.addEventListener(eventType, handler, false);
        else if (typeof element.attachEvent != "undefined")
            element.attachEvent("on" + eventType, function (event) {
                handler.call(element, event);
            });
        else
            throw new Error("Browser doesn't support addEventListener or attachEvent");
    }

    //version = "2.2.0rc" 版KO内部实现方式
    function writeValueToProperty(property, allBindingsAccessor, key, value) {
        if (!property || !ko.isWriteableObservable(property)) {
            var propWriters = allBindingsAccessor()['_ko_property_writers'];
            if (propWriters && propWriters[key])
                propWriters[key](value);
        } else if (property() !== value) {
            property(value);
        }
    }

    ko.bindingHandlers['dateString'] = {
        'init': function (element, valueAccessor, allBindingsAccessor) {
            if (element.nodeName != "INPUT") {
                return;
            }

            var valueUpdateHandler = function () {
                var modelValue = valueAccessor();
                var elementValue = element.value;
                writeValueToProperty(modelValue, allBindingsAccessor, 'dateString', elementValue);
            }

            registerEventHandler(element, "change", function () {
                valueUpdateHandler();
            });
        },
        'update': function (element, valueAccessor, allBindingsAccessor) {
            var pattern = allBindingsAccessor().datePattern || "yyyy-MM-dd";
            var newValue = ko.utils.unwrapObservable(valueAccessor());
            if ((newValue === null) || (newValue === undefined)) {
                newValue = "";
            }
            else if (newValue && typeof newValue == "string") {
                newValue = format.call(parseISO8601Date(newValue), pattern);
            }

            if (element.nodeName != "INPUT") {
                if (typeof jQuery != "undefined") {
                    jQuery(element).text(newValue);
                    return;
                }
                if (element.innerText !== undefined)
                    element.innerText = newValue;
                else {
                    var c = document.createTextNode(newValue);
                    if (element.firstChild) {
                        element.removeChild(element.firstChild);
                    }
                    element.appendChild(c);
                }
                return;
            }

            var elementValue = element.value;
            var valueHasChanged = (newValue != elementValue);

            if (valueHasChanged) {
                var applyValueAction = function () {
                    element.value = newValue;
                };
                applyValueAction();
            }
        }
    }
    ko.bindingHandlers['boolString'] = {
        'init': function (element, valueAccessor, allBindingsAccessor) {
            if (element.nodeName != "INPUT") {
                return;
            }

            var valueUpdateHandler = function () {
                var modelValue = valueAccessor();
                var elementValue = element.value;
                writeValueToProperty(modelValue, allBindingsAccessor, 'boolString', elementValue);
            }

            registerEventHandler(element, "change", function () {
                valueUpdateHandler();
            });
        },
        'update': function (element, valueAccessor, allBindingsAccessor) {
            var val = ko.utils.unwrapObservable(valueAccessor());
            var showText = val;
            if (val === true) {
                showText = "是";
            }
            else if (val === false) {
                showText = "否";
            }

            if (element.nodeName != "INPUT") {
                if (typeof jQuery != "undefined") {
                    jQuery(element).text(showText);
                    return;
                }
                if (element.innerText !== undefined)
                    element.innerText = showText;
                else {
                    var c = document.createTextNode(showText);
                    if (element.firstChild) {
                        element.removeChild(element.firstChild);
                    }
                    element.appendChild(c);
                }
                return;
            }

            var elementValue = element.value;
            var valueHasChanged = (showText != elementValue);

            if (valueHasChanged) {
                var applyValueAction = function () {
                    element.value = showText;
                };
                applyValueAction();
            }
        }
    }
    ko.bindingHandlers['boolChecked'] = {
        'init': function (element, valueAccessor, allBindingsAccessor) {
            if (element.nodeName != "INPUT" || element.type != 'radio') {
                throw new Error("boolChecked bind element must be radio input.");
                return;
            }

            var valueUpdateHandler = function () {
                var modelValue = valueAccessor();
                var elementValue = element.value; //alert(elementValue + "--" + modelValue());

                if (elementValue == "true")
                    elementValue = true;
                else if (elementValue == "false")
                    elementValue = false;
                else
                    elementValue = null;

                writeValueToProperty(modelValue, allBindingsAccessor, 'boolChecked', elementValue);
            }

            registerEventHandler(element, "change", function () {
                valueUpdateHandler();
            });
        },
        'update': function (element, valueAccessor, allBindingsAccessor) {
            var newValue = ko.utils.unwrapObservable(valueAccessor());

            var newValueString = newValue;
            if (newValue === true) {
                newValueString = "true";
            } else if (newValue === false) {
                newValueString = "false";
            }

            if (element.value == newValueString) {
                element.checked = true;
            }
            else {
                element.checked = false;
            }
        }
    }
    ko.bindingHandlers['typedChecked'] = {
        'init': function (element, valueAccessor, allBindingsAccessor) {
            var dataType = allBindingsAccessor().dataType || "string";
            if (element.nodeName != "INPUT" || element.type != 'radio') {
                throw new Error("typedChecked bind element must be radio input.");
                return;
            }

            var valueUpdateHandler = function () {
                var modelValue = valueAccessor();
                var newValue = element.value;

                if (element.value === null || element.value === "" || element.value === undefined)
                    newValue = null;
                else {
                    if (dataType == "bool") {
                        if (element.value === "true")
                            newValue = true;
                        else if (element.value === "false")
                            newValue = false;
                    }
                    else if (dataType == "number") {
                        newValue = parseFloat(element.value);
                    }
                }

                writeValueToProperty(modelValue, allBindingsAccessor, 'typedChecked', newValue);
            }

            registerEventHandler(element, "change", function () {
                valueUpdateHandler();
            });
        },
        'update': function (element, valueAccessor, allBindingsAccessor) {
            var dataType = allBindingsAccessor().dataType || "string";
            var newValue = ko.utils.unwrapObservable(valueAccessor());

            if (newValue === undefined)
                newValue = null;

            var elementValue = element.value;

            var newValueString = newValue;
            if (element.value === null || element.value === "" || element.value === undefined)
                elementValue = null;
            else {
                if (dataType == "bool") {
                    if (element.value === "true") {
                        elementValue = true;
                    } else if (element.value === "false") {
                        elementValue = false;
                    }
                }
                else if (dataType == "number") {
                    elementValue = parseFloat(element.value);
                }
            }

            if (elementValue == newValue) {
                element.checked = true;
            }
            else {
                element.checked = false;
            }
        }
    }


    ko.bindingHandlers.display = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            if (valueAccessor())
                $(element).modal('show');
            else
                $(element).modal('hide');
        },
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            ko.bindingHandlers.display.init(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext);
        }
    };


    var $ko = {};
    $ko.toOb = function (obj) {
        var ret;
        if (Object.prototype.toString.call(obj) === '[object Array]') {
            ret = ko.mapping.fromJS({ T: obj }).T();
        }
        else {
            ret = ko.mapping.fromJS(obj);
        }
        return ret;
    }
    $ko.toJS = function (obj) {
        return ko.mapping.toJS(obj);
    }
    $ko.updateModel = function (model, obj) {
        //ko.mapping.fromJS 有个坑，就是 viewModel 必须得是 ko.mapping.fromJS 来的，否则，更新不上
        //ko.mapping.fromJS(obj, viewModel);

        if (!isJsonObj(model))
            throw new Error("model 必须是一个非数组的 js 对象");

        for (var propName in obj) {
            if (!(propName in model))
                continue;

            var prop = model[propName];
            var newValue = obj[propName];
            if (ko.isObservable(prop)) {
                var oldVal = prop();
                if (!isJsonObj(oldVal) && !isJsonObj(newValue)) {
                    prop(newValue);
                }
                else if (isJsonObj(oldVal) && isJsonObj(newValue)) {
                    $ko.updateModel(oldVal, newValue);
                }
                else
                    throw new Error("不支持将 obj 对象更新 model");
            }
            else {
                if (!isJsonObj(prop) && !isJsonObj(newValue)) {
                    model[propName] = newValue;
                }
                else if (isJsonObj(prop) && isJsonObj(newValue)) {
                    $ko.updateModel(prop, newValue);
                }
                else
                    throw new Error("不支持将 obj 对象更新 model");
            }
        }
    }

    window.$ko = $ko;


    function isJsonObj(obj) {
        if (obj === null)
            return false;

        var isJson = typeof (obj) == "object" && Object.prototype.toString.call(obj).toLowerCase() == "[object object]" && !obj.length;
        return isJson;
    }
})();