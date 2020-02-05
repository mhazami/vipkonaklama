$(document).ready(function () {
    var list = $("*[id^='validate-']");
    for (index in list) {
        if (!list[index].id) continue;
        var id = list[index].id.replace("validate-", "");
        var control = document.getElementById(id);
        var group = $(list[index]).attr("group");
        if (group)
            validate(group);
        $(control).keyup(function () {
            group = $(list[index]).attr("group");
            if (group)
                validate(group);
        });
    }
});

function validate(group) {
    var result = true;
    var list = $("*[group='" + group + "']");
    for (index in list) {
        if (!list[index].id) continue;
        var id = list[index].id.replace("validate-", "");
        var control = document.getElementById(id);
        var exp = "/^\s+$/";
        if (control != null) {
            if (exp.match(control.value)) {
                $(list[index]).css("display", "inline");
                $(control).css("border", "solid red 1px;");
                result = false;
            } else {
                $(list[index]).css("display", "none");
                $(control).css("border", "solid black 1px;");
            }
        }
    }
    return result;
}