function initDoc(name) {
    var oContent = document.createTextNode(document.getElementById("Div-" + name).innerHTML.replace(/&nbsp;/gi, ' '));
    document.getElementById("Div-" + name).innerHTML = "";
    document.getElementById("Div-" + name).appendChild(oContent);
    $("#" + "BtnCloseFullScreen-" + name).hide();
    DivInnerHtmlChange(name);
    DesignMode(name);

    //paste event
    $('#Div-' + name).bind('paste', function (e) {
        if (IsSourceMode(name) == true) { //source mode
            //post paste event
            setTimeout(function () {
                //get text mode
                var htmlCode = $("#Div-" + name).text();
                //clean text data
                var htmlText = cleanInputHtml(htmlCode);
                //show html text
                $("#Div-" + name).text(htmlText);
            }, 700);
        } else { //design mode
            //if plain past mode was true
            if (IsPlainPasteMode(name) == true) {
                //post paste event
                setTimeout(function () {
                    //cleanWordTextByElement("Div-" + name);
                    $("#Div-" + name).html(cleanWordText($("#Div-" + name).html()));
                }, 700);
            }
        }
    });

    //change value
    $('#Div-' + name).bind("input blur focus", function (e) {
        //countWords(name);
        showWordCount(name);
        //countCharacters(name);
        showCharacterCount(name);
    });
}

function editorTextChangedCallback(name, callback) {

}

function countWords(name) {
    var s = $("#Div-" + name).text();
    var wordCount = 0;
    if (s.trim().length != 0) {
        s = s.replace(/\n+/gi, ' ');
        s = s.trim().replace(/\s+/gi, ' ');
        wordCount = s.split(' ').length;
    }

    return wordCount;
}

function showWordCount(name) {
    var wordCnt = countWords(name);
    if ($("#wordcount_" + name).length > 0) {
        $("#wordcount_" + name).html(wordCnt + " کلمه");
    }
}

function countCharacters(name) {
    var s = $("#Div-" + name).text();
    var wordCount = 0;
    if (s.length != 0) {
        //s = s.replace(/\n+/gi, ' ');
        //s = s.trim().replace(/\s+/gi, ' ');
        //wordCount = s.split(' ').length;
        wordCount = s.length;
    }

    return wordCount;
}

function showCharacterCount(name) {
    var chrsCnt = countCharacters(name);
    if ($("#wordchars_" + name).length > 0) {
        $("#wordchars_" + name).html(chrsCnt + " حرف");
    }
}

function DivInnerHtmlChange(name) {
    document.getElementById(name).innerHTML = "";
    var value = "";
    if (IsSourceMode(name)) {
        var oContent = document.createRange();
        oContent.selectNodeContents(document.getElementById("Div-" + name));
        var string = oContent.toString();
        string = string.replace(/&nbsp;/, ' ');
        value = document.createTextNode(string);
    } else {
        var htmlText = document.getElementById("Div-" + name).innerHTML;
        //htmlText = cleanWordText(htmlText);
        value = document.createTextNode(htmlText);
    }
    document.getElementById(name).innerHTML = value.nodeValue;
}

function replaceAll(baseString, find, replace) {
    var escapeRegExp = find.replace(/([.*+?^=!:${}()|\[\]\/\\])/g, "\\$1");
    //return baseString.replace(new RegExp(escapeRegExp, 'g'), replace);
    return baseString.replace(new RegExp(escapeRegExp, 'gi'), replace);
}

// removes MS Office generated guff
function cleanWordText(input) {
    var output = input;

    // 1. remove line breaks / Mso classes
    var stringStripper = /(\n|\r| class=(")?Mso[a-zA-Z]+(")?)/g;
    output = input.replace(stringStripper, ' ');
    // 2. strip Word generated HTML comments
    var commentSripper = new RegExp('<!--(.*?)-->', 'g');
    output = output.replace(commentSripper, '');
    //var tagStripper = new RegExp('<(/)*(meta|link|span|\\?xml:|st1:|o:|font)(.*?)>', 'gi');
    var tagStripper = new RegExp('<(/)*(meta|link|\\?xml:|st1:|o:|font)(.*?)>', 'gi');
    // 3. remove tags leave content if any
    output = output.replace(tagStripper, '');
    // 4. Remove everything in between and including tags '<style(.)style(.)>'
    //var badTags = ['style', 'script', 'applet', 'embed', 'noframes', 'noscript'];
    var badTags = ['style', 'script', 'applet', 'embed', 'noframes', 'noscript'];
    for (var i = 0; i < badTags.length; i++) {
        tagStripper = new RegExp('<' + badTags[i] + '.*?' + badTags[i] + '(.*?)>', 'gi');
        output = output.replace(tagStripper, '');
    }
    // 5. remove attributes ' style="..."'
    //var badAttributes = ['style', 'start', 'lang', 'v:shapes'];
    var badAttributes = ['start', 'lang', 'v:shapes'];
    for (var k = 0; k < badAttributes.length; k++) {
        tagStripper = new RegExp(' ' + badAttributes[k] + '="(.*?)"', 'gi');
        output = output.replace(tagStripper, '');
    }
    //7. remove bad inline style tag
    var badStyleTags = [
        'unicode-bidi',
        'mso-ascii-font-family',
        'mso-ascii-theme-font',
        'mso-hansi-font-family',
        'mso-bidi-font-family',
        'mso-bidi-theme-font',
        'mso-bidi-language',
        'mso-themecolor',
        'mso-themeshade',
        'mso-style-textfill-fill-color',
        'mso-style-textfill-fill-themecolor',
        'mso-style-textfill-fill-alpha',
        'mso-style-textfill-fill-colortransforms',
        'mso-hansi-theme-font',
        'mso-ignore',
        'mso-list',
        'mso-spacerun',
        'text-kashida',
    ];
    for (var j = 0; j < badStyleTags.length; j++) {
        //var regCmd = new RegExp('(?:|[^"]*[;\s])(' + badStyleTags[j] + '\s*:[^";]*)', 'gi');
        //var regCmd = new RegExp('(' + badStyleTags[j] + '\s*:[^";]*)', 'gi');
        var regCmd = new RegExp('(' + badStyleTags[j] + '\s*:[^";]*(;|))', 'gi');
        output = output.replace(regCmd, '');
    }
    // 8.remove '<!-- -->'
    output = output.replace(/\n\n/g, "<br />").replace(/.*<!--.*-->/g, "");

    //9.remove bad tags
    output = output.replace(/.*<!--.*-->/g, ' ');
    output = output.replace(/.*<[.*]>/g, ' ');
    //10.style importve
    var styleAttrib = /\&quot\;/g;
    output = output.replace(styleAttrib, '');

    return output;
}

function cleanInputHtml(input) {
    var output = input;
    try {
        // 1. remove line breaks / Mso classes
        output = input.replace(/(\n)/g, ' ');

        // 1. remove line breaks / Mso classes
        output = output.replace(/(\s)/g, ' ');
    } catch (e) {

    }

    return output;
}

function formatDoc(name, sCmd, sValue) {
    if (validateMode(name)) {
        document.execCommand(sCmd, false, sValue);
        document.getElementById("Div-" + name).focus();
        DivInnerHtmlChange(name);
    }
}

function validateMode(name) {
    if (!IsSourceMode(name)) { return true; }
    //    alert("Uncheck \"Show HTML\".");
    alert("Please Select \"Design Mode\".");
    document.getElementById("Div-" + name).focus();
    return false;
}

function IsSourceMode(name) {
    var val = document.getElementById("SourceMode-" + name).value;
    if (val == "true")
        return true;
    else
        return false;
}

function IsPlainPasteMode(name) {
    return true;

    if (document.getElementById("PlainPaste-" + name) == null) {
        return false;
    }
    var val = document.getElementById("PlainPaste-" + name).value;
    if (val == "true")
        return true;
    else
        return false;
}

function DesignMode(name) {
    if (IsSourceMode(name)) {
        document.getElementById("SourceMode-" + name).value = false;

        try {
            var textHtml = $("#Div-" + name).text();
            $("#Div-" + name).html(textHtml);
        }
        catch (e) { }


        document.getElementById("Div-" + name).focus();
        $("#BtnSourceMode-" + name).removeClass("editortypeSelect");
        $("#BtnDesignMode-" + name).addClass("editortypeSelect");
    }
}

function SourceMode(name) {
    if (!IsSourceMode(name)) {

        document.getElementById("SourceMode-" + name).value = true;

        try {
            var html = $("#Div-" + name).html();
            //document.getElementById("Div-" + name).innerText = html;
            $("#Div-" + name).text(html);
            //showSource(html);
        }
        catch (e) { }



        document.getElementById("Div-" + name).focus();
        $("#BtnSourceMode-" + name).addClass("editortypeSelect");
        $("#BtnDesignMode-" + name).removeClass("editortypeSelect");
    }

    function fixCR(str) { return String(str).replace(/\n/gi, '<br />\n') }
    function fixHTML(str) { return String(str).replace(/&/gi, '&#38;').replace(/</gi, '&#60;') }
}

function showSource(textEdit) {
    var win = window.open('', '', 'scrollbars=yes,resizable,width=500,height=500');
    //win.document.write('<div style="white-space:nowrap">&lt;html>' + fixCR(fixHTML(document.documentElement.innerHTML)) + '&lt;/html></div>');
    win.document.write('<div style="white-space:nowrap">' + fixCR(fixHTML(textEdit)) + '</div>');

    function fixCR(str) { return String(str).replace(/\n/gi, '<br />\n') }

    function fixHTML(str) { return String(str).replace(/&/gi, '&#38;').replace(/</gi, '&#60;') }
}

function Clean(name) {
    if (validateMode(name) && confirm('Are you sure?')) {
        document.getElementById("Div-" + name).innerHTML = "";
        DivInnerHtmlChange(name);
    }
}

function printDoc(name) {
    if (!validateMode(name)) {
        return;
    }
    var oPrntWin = window.open("", "_blank", "width=450,height=470,left=400,top=100,menubar=yes,toolbar=no,location=no,scrollbars=yes");
    oPrntWin.document.open();
    oPrntWin.document.write("<!doctype html><html><head><title>Print<\/title><\/head><body onload=\"print();\">" + document.getElementById("Div-" + name).innerHTML + "<\/body><\/html>");
    oPrntWin.document.close();
}

function ShowDialogHtml(name, id, width, height) {
    $("#" + id + name).dialog({
        show: {
            effect: "blind",
            duration: 100
        },
        hide: {
            effect: "explode",
            duration: 100
        },
        resizable: true,
        height: height,
        width: width,
        modal: true,
        buttons: {
            "Close": function () {
                $("#" + id + name).html("");
                $(this).dialog("close");
            }
        }
    });
}

function UploadFile(name, folderid, appendhtml) {
    $("[id^=DivLoadUpload-]").each(function () {
        $(this).html("");
    });
    $.get("/FileManager/RadynEditor/EditorFolder", { editorId: name, folderId: folderid, appendhtml: appendhtml, date: new Date().getTime() }, function (data) {
        if (data != "false") {
            ShowDialogHtml(name, "DivLoadUpload-", 900, 350);
            $("#DivLoadUpload-" + name).html(data);
            $("#DivLoadUpload-" + name).slideDown();
        }
        else ShowRadynMessage();
    });
}

function InsertLink(name, folderid) {
    $("[id^=DivLoadInsertLink-]").each(function () {
        $(this).html("");
    });
    $.get("/FileManager/RadynEditor/InsertLink", { editorId: name, folderId: folderid, date: new Date().getTime() }, function (data) {
        if (data != "false") {
            ShowDialogHtml(name, "DivLoadInsertLink-", 500, 400);
            $("#DivLoadInsertLink-" + name).html(data);
            $("#DivLoadInsertLink-" + name).slideDown();


        } else ShowRadynMessage();
    });

}

function ShowFullScreen(name) {
    $("#DivMaster-" + name).addClass("editor-mass");
    $("#BtnFullScreen-" + name).hide();
    $("#BtnCloseFullScreen-" + name).show();
    $("#DivMasterBtn-" + name).addClass("editortype-mass");

}

function CloseFullScreen(name) {
    $("#DivMaster-" + name).removeClass();
    $("#BtnCloseFullScreen-" + name).hide();
    $("#BtnFullScreen-" + name).show();
    $("#DivMasterBtn-" + name).removeClass("editortype-mass");
}

function ApppendFileUrl(name, url, appendhtml) {
    if (appendhtml) {
        if (url != "") {
            document.getElementById("Div-" + name).innerHTML += url;
            DivInnerHtmlChange(name);
        }
    }
    $("#DivLoadUpload-" + name).html("");
    $("#DivLoadUpload-" + name).dialog("close");

}

function ApppendLink(name, linkurl, text) {
    if (linkurl != "") {
        if (text != "" && document.getElementById("Div-" + name).innerHTML.indexOf(text) !== -1) {
            document.getElementById("Div-" + name).innerHTML =
                document.getElementById("Div-" + name).innerHTML.replace(text, linkurl);
        }
        else
            document.getElementById("Div-" + name).innerHTML += linkurl;
        DivInnerHtmlChange(name);
    }
    $("#DivLoadInsertLink-" + name).html("");
    $("#DivLoadInsertLink-" + name).dialog("close");
}