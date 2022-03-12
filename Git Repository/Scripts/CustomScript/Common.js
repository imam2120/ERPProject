

function CallAjax_POST(url, param, IsAsync, callBackFunc) {
    var _data;
    $ajaxRequest = $.ajax({
        url: url,
        data: param,
        type: 'POST',
        dataType: "json",
        async: IsAsync,
        success: function (sdata) {
            if (typeof callBackFunc != 'undefined') {
                if (!callBackFunc) {
                    var result = {};
                    //alert(JSON.stringify(param));
                    $.extend(result, sdata, param);
                } else {
                    var result = {};
                    $.extend(result, sdata, param);
                    AsyncAjaxCallback(result);
                }
            }
            _data = sdata;
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //GetAjaxErrorAlert(xhr);
        }
    });
    return _data;
}