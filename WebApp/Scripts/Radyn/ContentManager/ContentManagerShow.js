
$(document).ready(function () {
    GetPartials();
    GetDesign();
});
function GetPartials() {
    $("#partialId").html("");
    $.get("/ContentManager/UIPanel/GetPartials", { date: new Date().getTime() }, function (data) {
        for (var p in data) {
            $("#partialId").append("<option value='" + data[p].id + "'  text='" + data[p].title + "' >" + data[p].title + "</option>");
        }
    });

}

function GetDesign() {
    $.get("/ContentManager/UIPanel/GetSavedDesign", { date: new Date().getTime() }, function (data) {
        if (data.result != false) {
            $("#divInnerHtml").html(data.bodytext);
            $("#divInnerHtml").slideDown();
            AssginDesign();
        }
    });
}
function AssginDesign() {
    $.get("/ContentManager/UIPanel/AssginSavedHtml", { date: new Date().getTime() }, function (data) {
        if (data != null) {
            for (index in data) {
                ReturnHtmldata(data[index].partialid, data[index].customid, data[index].Position);
            }
        }
    });
}
function ReturnHtmldata(partialid, customid, position) {
   
    $.get("/ContentManager/UIPanel/ReturnHtml", { pid: partialid, date: new Date().getTime() }, function (data) {
        if (data != "") {
            switch (position) {
                case 0:
                    {
                        $("[customId=" + customid + "]").append(data);
                        $("[customId=" + customid + "]").slideDown();
                    }
                    break;
                case 1:
                    {
                        $("[customId=" + customid + "]").prepend(data);
                        $("[customId=" + customid + "]").slideDown();
                    }
                    break;
                default:
            }
           

        }
    });
}
