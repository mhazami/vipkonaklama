$(document).ready(function () {
    $("#state").val("s");
    var state = getQueryString("state");
    if (state != undefined) {
        if (state == "a") {
            $("#state").val("a");
        }
        else if (state == "s") {
            $("#state").val("s");
        }
    }
    $("#hid-type").val("l");
    $("#r-legal").attr("checked", "checked");
    var type = getQueryString("type");
    if (type == "l") {
        $("#state").val("a");
        $("#r-legal").attr("checked", "checked");
        $("#hid-type").val(type);
    }
    if (type == "r") {
        $("#state").val("a");
        $("#r-real").attr("checked", "checked");
        $("#hid-type").val(type);
    }
    ChangeType();
    ChangeState();

    $("tr[name^='tr-']").click(function () {
        alert("tt");
    });
});

function ChangeState() {
    var state = $("#state").val();
    switch (state) {
        case "s":
            $("#a-search").text("جستجوي پيشرفته");
            $("#state").val("a");
            $("#a-div").slideUp('slow').delay(2000);
            $("#s-div").slideDown('slow');
            break;
        case "a":
            $("#a-search").text("جستجوي سريع");
            $("#state").val("s");
            $("#s-div").slideUp('slow').delay(2000);
            $("#a-div").slideDown('slow');
            break;
    }
}

function ChangeType() {
    var ty = $("#hid-type").val();
    switch (ty) {
        case "r":
            $("#div-legal").slideUp('slow').delay(2000);
            $("#div-real").slideDown('slow');
            break;
        case "l":
            $("#div-real").slideUp('slow').delay(2000);
            $("#div-legal").slideDown('slow');
            break;
    }

}

function checkType(radio) {
    if (radio.checked) {
        if (radio.id.indexOf("real") > 0)
            $('#hid-type').val('r');
        else if (radio.id.indexOf("legal") > 0)
            $('#hid-type').val('l');
        ChangeType();
    }
}


function addRow(node) {
    var table = document.getElementById("grid");
    var cellCount = 0;
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);
    if (rowCount % 2 == 0)
        row.setAttribute("name", "tr-norm");
    else
        row.setAttribute("name", "tr-alt");
    if (rowCount == 0)
        row.setAttribute("name", "head");
    var rowCell = new Array();

    rowCell[0] = row.insertCell(0);
    rowCell[0].align = "center";
    rowCell[0].width = "150px";
    rowCell[0].innerText = node.registerNo == null ? "" : node.registerNo;

    rowCell[1] = row.insertCell(0);
    rowCell[1].align = "center";
    rowCell[1].width = "150px";
    rowCell[1].innerText = node.nationalCode == null ? "" : node.nationalCode;


    rowCell[2] = row.insertCell(0);
    rowCell[2].align = "center";
    rowCell[2].width = "80px";
    rowCell[2].innerText = node.type;


    rowCell[3] = row.insertCell(0);
    rowCell[3].align = "center";
    rowCell[3].width = "150px";
    rowCell[3].innerText = node.title;

    rowCell[4] = row.insertCell(0);
    rowCell[4].align = "center";
    rowCell[4].width = "50px";
    if (rowCount > 0) {
        rowCell[4].innerHTML = "<input type='button' onclick=\"showModalReturn('" + node.id + "','" + node.title + "');\" value='انتخاب'/>";
    }
}
