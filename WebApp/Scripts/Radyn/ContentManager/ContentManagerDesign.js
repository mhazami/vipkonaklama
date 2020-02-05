
$(document).ready(function () {
    GetPartials();
    GetDesign();

});

function SaveDesign() {
    var divname = document.getElementById("modelId").value;
    var partialid = document.getElementById("partialId").value;
    var stateId = document.getElementById("StateId").value;
    var HtmlId = document.getElementById("HtmlId").value;
    $("[customId=" + divname + "]").html(" ");
    if (partialid != "") {
        $.get("/ContentManager/UIPanel/SaveDesign", { partialId: partialid, customId: divname, position: stateId,htmlId:HtmlId, date: new Date().getTime() }, function (data) {
            GetDesign();
            ShowRadynMessage();
        });
    }
}

function GetPartials() {
    $("#partialId").html("");
    var PTId = document.getElementById("PartialTypeId").value;
    $.get("/ContentManager/UIPanel/GetPartials", { TypeId: PTId, date: new Date().getTime() }, function (data) {
        for (var p in data) {
            $("#partialId").append("<option  value='" + data[p].id + "'    text='" + data[p].title + "' >" + data[p].title + "</option>");
            
        }
    });

}



function GetDesign() {
    $.get("/ContentManager/UIPanel/GetSavedDesign", { date: new Date().getTime() }, function (data) {
        if (data.result != false) {
            $("#divInnerHtml").html(data.bodytext);
            $("#divInnerHtml").slideDown();
            AssginNameDesign();
            AssginDesign();
        }
    });
}
function AssginDesign() {
    $.get("/ContentManager/UIPanel/AssginSavedHtml", { date: new Date().getTime() }, function (data) {
        if (data != null) {
            for (index in data) {
                $("[customId=" + data[index].customid + "]").html("");
                $("[customId=" + data[index].customid + "]").removeAttr("style");
                $("[customId=" + data[index].customid + "]").css("border", "1px solid #000");
                ReturnHtmldata(data[index].partialid, data[index].customid, data[index].Position);
            }
        }
    });
}
function boldelement() {
    var divname = document.getElementById("modelId").value;
    $("[customId=" + divname + "]").css('border', '5px solid #000');
    $("[customId=" + divname + "]").delay(5000).show(0, function () {
        $("[customId=" + divname + "]").css('border', '1px solid #000');
    });
}
function AssginNameDesign() {
    $.get("/ContentManager/UIPanel/AssginNameHtml", { date: new Date().getTime() }, function (data) {
        if (data != null) {
            for (index in data) {
                $("[customId=" + data[index].customid + "]").append(data[index].title).css("text-align", "center");
                $("[customId=" + data[index].customid + "]").css("border", "1px solid #000");
            }
        }
    });
}

function ShowDesgin() {
    window.open("/Home/index");
}
function ReturnHtmldata(partialid, customid, position) {

    $.get("/ContentManager/UIPanel/ReturnHtml", { pid: partialid, date: new Date().getTime() }, function (data) {
        if (data != "") {
            switch (position) {
                case 1:
                    {
                        $("[customId=" + customid + "]").prepend(data);
                        $("[customId=" + customid + "]").slideDown();
                      
                    }
                    break;
                case 0:
                    {
                        $("[customId=" + customid + "]").append(data);
                        $("[customId=" + customid + "]").slideDown();
                    }
                    break;
                default:
            }


        }
    });
}
function ClearDesign() {
    var divname = document.getElementById("modelId").value;
    var partialid = document.getElementById("partialId").value;
    $.get("/ContentManager/UIPanel/ClearDesign", { partialId: partialid, customId: divname, date: new Date().getTime() }, function (data) {
        if (data != "false") {
            GetDesign();
        }
        ShowRadynMessage();
    });

}