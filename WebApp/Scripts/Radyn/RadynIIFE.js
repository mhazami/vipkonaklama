
function ShowModal(url, data) {
    PrepareShowModal(url, data, true, false);
};
function ShowModalInOtherTag(url, tag, data) {
    PrepareShowModal(url, data, true, false, tag);
};
function ShowModalFullScreen(url, data) {
    PrepareShowModal(url, data, true, true);
};

function getPartial(url, data, el) {
    PreparegetPartial(url, data, el, false);
}

function ShowModalWithoutFormTag(url, data) {
    PrepareShowModal(url, data, false, false);
};


function getPartialWithFormTag(url, data, el) {
    PreparegetPartial(url, data, el, true);
}

function modal_close_getPartial(url, data) {
    Close_thisModal();
    getPartial(url, data);
}

function Close_thisModal() {
    var index = 1;
    var validId = $("#RadynFormId").val();
    var countform = $("input[Id='RadynFormId']").length;
    $("[Id='RadynFormId']").each(function () {
        if (index == countform) {
            validId = $(this).val();
            return;
        }
        index++;
    });
  
    $("#Div" + validId).html("");
    $("#" + validId).modal("hide");
    $("body").removeClass("modal-open");
   
    

}
function GetThisFormUrl() {

    var index = 1;
    var validId = $("#RadynFormUrl").val();
    var countform = $("input[Id='RadynFormUrl']").length;
    $("[Id='RadynFormUrl']").each(function () {
        if (index == countform) {
            validId = $(this).val();
            return;
        }
        index++;
    });

    return validId;



}
function PrepareShowModal(url, data, addfromtag, fullScreen, tag) {
    ShowPostBackLoader();
    if ($(".modal.fade").hasClass("in")) {
        $("body").addClass("main-modal-open");
    } else {
        $("body").removeClass("main-modal-open");
    }
    url = FixUrl(url, data, addfromtag, fullScreen);
    $.get("/CommonUI/GetModal", { url: url, date: new Date().getTime() }, function (data) {
        if (tag != null && tag != undefined)
            $("#" + tag).append(data);
        else
            $("#ModalForm").append(data);
    }).always(function () {
        ShowRadynMessage();
        HidePostBackLoader();
    });

};

function PreparegetPartial(url, data, el, addfromtag) {
    if (url == undefined || url === "") return;
    ShowPostBackLoader();
    url = FixUrl(url, data, addfromtag);
    $.get("/CommonUI/GetPartial", { url: url, date: new Date().getTime() }, function (response) {
        if (el != null && el !== undefined) {
            $('div[id="' + el + '"]').html('');
            $('div[id="' + el + '"]').html(response);

        }
        else {
            $("#div-box").html('');
            $("#div-box").html(response);

        }
    }).always(function () {
        ShowRadynMessage();
        HidePostBackLoader();
    });

}


function ShowRadynMessage(message) {
    if (message != undefined)
        $.get("/CommonUI/SetMessage", { message: message, id: new Date().getTime() }, function (data) { });
    $.get("/CommonUI/GetPopMessage", { id: new Date().getTime() }, function (data2) {
        if (data2 != undefined && data2.Message != "") {
            $('#radynMessage').html(data2.Message);
        }

    });

}
function RadynGet(geturl, data, afterfunction) {
    ShowPostBackLoader();
    $.get(geturl, data, afterfunction).always(function () {
        ShowRadynMessage();
        HidePostBackLoader();
    });
}
function RadynGetWithalways(geturl, data, afterfunction, alwaysfunction) {
    ShowPostBackLoader();
    $.get(geturl, data, afterfunction).always(function () {
        if (alwaysfunction != undefined)
            eval(alwaysfunction);
        ShowRadynMessage();
        HidePostBackLoader();
    });
}
function RadynPostForm(posturl, formurl, afterfunction) {
    ShowPostBackLoader();
    $.post(posturl, $('form[action^="' + formurl + '"]').serialize(), afterfunction).always(function () {
        ShowRadynMessage();
        HidePostBackLoader();
    });
}
function RadynPostFormWithalways(posturl, formurl, afterfunction, alwaysfunction) {
    ShowPostBackLoader();
    $.post(posturl, $('form[action^="' + formurl + '"]').serialize(), afterfunction).always(function () {
        if (alwaysfunction != undefined)
            eval(alwaysfunction);
        ShowRadynMessage();
        HidePostBackLoader();
    });
}
function RadynPostThisForm(posturl, afterfunction) {
    ShowPostBackLoader();
    var formurl = GetThisFormUrl();
    $.post(posturl, $('form[action^="' + formurl + '"]').serialize(), afterfunction).always(function () {
        ShowRadynMessage();
        HidePostBackLoader();
    });
}
function RadynPostThisFormWithalways(posturl, afterfunction, alwaysfunction) {
    ShowPostBackLoader();
    var formurl = GetThisFormUrl();
    $.post(posturl, $('form[action^="' + formurl + '"]').serialize(), afterfunction).always(function () {
        if (alwaysfunction != undefined)
            eval(alwaysfunction);
        ShowRadynMessage();
        HidePostBackLoader();
    });
}

function RadynPostData(posturl, data, afterfunction) {
    ShowPostBackLoader();
    $.post(posturl, data, afterfunction).always(function () {
        ShowRadynMessage();
        HidePostBackLoader();
    });
}
function RadynPostDataWithalways(posturl, data, afterfunction, alwaysfunction) {
    ShowPostBackLoader();
    $.post(posturl, data, afterfunction).always(function () {
        eval(alwaysfunction);
        ShowRadynMessage();
        HidePostBackLoader();
    });
}

function ShowPostBackLoader(time) {
    $(".loading-box").css("display", "block");
    if (time != "" && time != undefined) {
        setTimeout(HidePostBackLoader(), time);
    }

}
function HidePostBackLoader() {
    $(".loading-box").css("display", "none");

}

function FixUrl(url, data, addfromtag, fullScreen) {
    if (url == undefined || url === "")
        return null;
    var validurl = url;
    if (data != undefined && data != "") {
        var res = "";
        if ($.type(data) != "string") {
            data = JSON.stringify(data).replace(/"/g, "");

        }
        res = data.replace(/:/g, "=");
        res = res.replace(/{/g, "");
        res = res.replace(/}/g, "");
        res = res.replace(/,/g, "&");
        res = $.trim(res);
        if (res != "" && res != undefined) {
            var arrAnd = res.split('&');
            for (var i = 0; i < arrAnd.length; i++) {
                arrAnd[i] = $.trim(arrAnd[i]);
            }
            res = arrAnd.join('&');

            var arrEqual = res.split('=');
            for (var i = 0; i < arrEqual.length; i++) {
                arrEqual[i] = $.trim(arrEqual[i]);
            }
            res = arrEqual.join('=');
            if (!url.endsWith("?") && !res.startsWith("?"))
                validurl = url + "?" + res;
            else
                validurl = url + res;
        }

    }
    if (!(validurl.indexOf("addformtag") > 0)) {
        if (validurl.indexOf("?") <= 0)
            validurl = validurl + "?addformtag=" + addfromtag;
        else
            validurl = validurl + "&addformtag=" + addfromtag;
    }
    if (fullScreen != undefined && !(validurl.indexOf("fullScreen") > 0)) {
        if (validurl.indexOf("?") <= 0)
            validurl = validurl + "?fullScreen=" + fullScreen;
        else
            validurl = validurl + "&fullScreen=" + fullScreen;
    }
    return validurl;
}

function Slide(id) {
    $("#" + id).slideToggle(500);
}

function arrow(id) {

    $("#" + id).toggleClass('f-plus f-minus');
    $(".f-plus").each(function () {
        $(this).css("background-image", "url('/images/f-plus.png')");
    });
    $(".f-minus").each(function () {
        $(this).css("background-image", "url('/images/f-minus.png')");
    });
}


function showModalReturn(id, desc, command) {
    var fki = $("#fki").val();
    var fkd = $("#fkd").val();
    var fkb = $("#fkb").val();
    if (fki != "" && fki != undefined && fkd != "" && fkd != undefined) {
        $("#" + fki).val(id);
        $("#" + fkd).val(desc);
    }

    if (command != "" && command != undefined)
        eval(command);
    Close_thisModal();
    if (fkb != "" && fkb != undefined)
        $("#" + fkb).click();

}



function ShowModalWithReturnValue(url, hidId, txtDesc) {
    var querystring = "";
    if (url.indexOf("?") > 0) querystring += "&";
    else querystring += "?";
    querystring += "fki=" + hidId;
    querystring += "&fkd=" + txtDesc;
    ShowModal(url + querystring);
}
function ShowModalInOtherTagWithReturnValue(url, tag, hidId, txtDesc) {
    var querystring = "";
    if (url.indexOf("?") > 0) querystring += "&";
    else querystring += "?";
    querystring += "fki=" + hidId;
    querystring += "&fkd=" + txtDesc;
    ShowModalInOtherTag(url + querystring, tag);
}


function ShowModalWithReturnValueWithEvents(url, hidId, txtDesc, evenetclick) {
    var querystring = "";
    if (url.indexOf("?") > 0) querystring += "&";
    else querystring += "?";
    querystring += "fki=" + hidId;
    querystring += "&fkd=" + txtDesc;
    querystring += "&fkb=" + evenetclick;
    ShowModal(url + querystring);

}
function ShowModalInOtherTagWithReturnValueWithEvents(url, tag, hidId, txtDesc, evenetclick) {
    var querystring = "";
    if (url.indexOf("?") > 0) querystring += "&";
    else querystring += "?";
    querystring += "fki=" + hidId;
    querystring += "&fkd=" + txtDesc;
    querystring += "&fkb=" + evenetclick;
    ShowModalInOtherTag(url + querystring, tag);

}



