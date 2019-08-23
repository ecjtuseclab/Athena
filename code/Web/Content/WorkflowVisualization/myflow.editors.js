(function ($) {
    var myflow = $.myflow;

    $.extend(true, myflow.editors,
{

    startTimeEditor: function () {
        var _props, _k, _div, _src, _r;
        this.init = function (props, k, div, src, r) {

            _props = props; _k = k; _div = div; _src = src; _r = r;

            $('<input id="startTime" style="width:100%;" class="demo-input"  />').val(props[_k].value).change(function () {
                props[_k].value = $(this).val();
            }).appendTo('#' + _div);

            $('#' + _div).data('editor', this);
            laydate.render({
                elem: '#startTime' //指定元素
            });
        }
        this.destroy = function () {
            $('#' + _div + ' input').each(function () {
                _props[_k].value = $(this).val();
            });
        }
    },
    endTimeEditor: function () {
        var _props, _k, _div, _src, _r;
        this.init = function (props, k, div, src, r) {

            _props = props; _k = k; _div = div; _src = src; _r = r;

            $('<input id="endTime" style="width:100%;" class="demo-input"  />').val(props[_k].value).change(function () {
                props[_k].value = $(this).val();
            }).appendTo('#' + _div);

            $('#' + _div).data('editor', this);
            laydate.render({
                elem: '#endTime' //指定元素
            });
        }
        this.destroy = function () {
            $('#' + _div + ' input').each(function () {
                _props[_k].value = $(this).val();
            });
        }
    },
    inputEditor: function () {
        var _props, _k, _div, _src, _r;
        this.init = function (props, k, div, src, r) {

            _props = props; _k = k; _div = div; _src = src; _r = r;

            $('<input style="width:100%;"/>').val(props[_k].value).change(function () {
                props[_k].value = $(this).val();
            }).appendTo('#' + _div);

            $('#' + _div).data('editor', this);
        }
        this.destroy = function () {
            $('#' + _div + ' input').each(function () {
                _props[_k].value = $(this).val();
            });
        }
    },
    selectEditor: function (arg) {
        var _props, _k, _div, _src, _r;
        this.init = function (props, k, div, src, r) {
            _props = props; _k = k; _div = div; _src = src; _r = r;
            if (props[_k].value === "") {
                if (arg.length == 0)
                {
                    var entity = {};
                    entity["value"] = -1;
                    entity["name"] = '-nodata-';
                    arg.push(entity);
                }
                props[_k].value = arg[0].value;
            }
            var sle = $('<select  style="width:100%;"/>').val(props[_k].value).change(function () {
                props[_k].value = $(this).val();
            }).appendTo('#' + _div);

            if (typeof arg === 'string') {
                $.ajax({
                    type: "GET",
                    url: arg,
                    success: function (data) {
                        var opts = eval(data);
                        if (opts && opts.length) {
                            props[_k].value = opts[0].value;
                            for (var idx = 0; idx < opts.length; idx++) {

                                sle.append('<option value="' + opts[idx].value + '">' + opts[idx].name + '</option>');
                            }
                            sle.val(_props[_k].value);
                        }
                    }
                });
            } else {
                for (var idx = 0; idx < arg.length; idx++) {

                    sle.append('<option value="' + arg[idx].value + '">' + arg[idx].name + '</option>');
                }
                sle.val(_props[_k].value);
            }

            $('#' + _div).data('editor', this);
        };
        this.destroy = function () {
            $('#' + _div + ' input').each(function () {
                _props[_k].value = $(this).val();
            });
        };
    }
});

})(jQuery);