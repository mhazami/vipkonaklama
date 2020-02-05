/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
//-- version 1.0.4.0
var Radyn;
(function (Radyn) {
    //--- Declare Class Names ----------------------------
    var currentPageClassName = "current";
    var pageButtonClassName = "paginate_button";
    var nextButtonClassName = "next";
    var prevButtonClassName = "previous";
    var firstButtonClassName = "first";
    var lastButtonClassName = "last";
    var lastPageButtonClassName = "last_page";
    var searchInputClassName = "search_textbox";
    var filterInputClassName = "filter_textbox";
    var sortableColumnsClassName = "sortable_column";
    var filterTypeItemClassName = "filter_type_item";
    var filterTypeButtonClassName = "filter_type_button";
    var isKeyColumnClassName = "is_key";
    var tableRowClassName = "table_row";
    var invisibleColumnClassName = "invisible";
    var rootGridClassName = "radyn_grid";
    var columnHeaderClassName = "column_header";
    var columnFooterClassName = "column_footer";
    var sortedColumnClassName = "sorted";
    var unsortedColumnClassName = "unsorted";
    var sortOrderAscClassName = "sort_order_asc";
    var sortOrderDescClassName = "sort_order_desc";
    var filteringRecordClassName = "filtering_record";
    var tableInfoClassName = "table_info";
    var pagenaviSectionClassName = "paginate_buttons";
    var radynLoaderClassName = "paginate_buttons";
    var searchAjaxParameterName = "s";
    //-------------------------------------------------------
    var templateFieldHtmlTag = "radyn";
    //-------------------------------------------------------
    var trElementIdNamePerfix = "tr";
    var columnHeaderIdNamePerfix = "column_header_";
    var columnFooterIdNamePerfix = "column_footer_";
    //-------------------------------------------------------
    var pageIndexAttrib = "pg_idx";
    var dataBoundAttrib = "dt_bound";
    //var dataCommandAttrib = "dt_cmd";
    var gridCommandAttrib = "gd_cmd";
    var filterTypeAttrib = "flt_typ";
    //var keyColumnAttrib = "flt_typ";
    var rowNumberAttrib = "row_num";
    var keysAttrib = "keys";
    var farsiLang = "fa-IR";
    var englishLang = "en-US";
    var turkLang = "tr-TR";
    var GridColumn = (function () {
        function GridColumn() {
        }
        return GridColumn;
    })();
    Radyn.GridColumn = GridColumn;
    var Callbacks = (function () {
        function Callbacks() {
        }
        return Callbacks;
    })();
    Radyn.Callbacks = Callbacks;
    var GridOptions = (function () {
        function GridOptions() {
            this.show_table_header = true;
            this.callbacks = new Callbacks();
        }
        return GridOptions;
    })();
    Radyn.GridOptions = GridOptions;
    var GridDataPack = (function () {
        function GridDataPack(elementId) {
            this.loadingCounter = 0;
            this.id = elementId;
            this.options = new GridOptions();
            this.data = [];
        }
        return GridDataPack;
    })();
    var Grid = (function () {
        function Grid(elementId, gridOptions) {
            this.gridId = elementId;
            this.initGrid(elementId, gridOptions);
        }
        //constructor
        //initGrid(gridId, gridOptions);
        //--- Main Methods ---------------------------------
        Grid.prototype.initGrid = function (id, options) {
            var _this = this;
            //load after page load completed
            $(document).ready(function () {
                if (options != null) {
                    _this.clearGlobalData();
                }
                var isNewGrid = !_this.isGridCreated();
                //sync data with jquery data
                _this.source = _this.syncWithGlobalData();
                //baraye load option dar zamani ke grid jadid bashad
                if (options != null) {
                    _this.source.options = options;
                }
                if (isNewGrid) {
                    // load Html grid
                    _this.loadHtmlGrid();
                }
            });
        };
        Grid.prototype.isGridCreated = function () {
            var element = document.getElementById(this.gridId);
            var d = jQuery.data(element);
            if (d != null && !jQuery.isEmptyObject(d)) {
                return true;
            }
            return false;
        };
        Grid.prototype.syncWithGlobalData = function () {
            var element = document.getElementById(this.gridId);
            var d = jQuery.data(element);
            if (d == null || jQuery.isEmptyObject(d)) {
                this.source = new GridDataPack(this.gridId);
                this.setOnGlobalData();
            }
            var exsitedSource = jQuery.data(element, "datapack");
            return exsitedSource;
        };
        Grid.prototype.setOnGlobalData = function () {
            jQuery.data(document.getElementById(this.gridId), "datapack", this.source);
        };
        Grid.prototype.clearGlobalData = function () {
            var element = document.getElementById(this.gridId);
            if (jQuery.hasData(element)) {
                jQuery.removeData(element);
            }
        };
        Grid.prototype.selectData = function (options, callbackDone) {
            var _this = this;
            if (this.source.data != null && this.source.data.length != 0) {
                this.clearSourceData();
            }
            //
            if (options != null) {
                this.source.options = options;
                // load Html grid
                this.loadHtmlGrid();
            }
            //get ajax information from options
            var url = this.source.options.select_url;
            var methodParam = this.source.options.select_url_params;
            if (url == null || url.length == 0) {
                this.importData([], callbackDone);
                return;
            }
            var promise = this.getDataByAjax(url, methodParam);
            promise.done(function (res) {
                _this.importData(res);
            });
            promise.fail(function (err) {
                if (callbackDone != null) {
                    var e = {
                        status: "error"
                    };
                    callbackDone(e);
                }
            });
            promise.always(function (st) {
                _this.showLoader(false);
            });
        };
        Grid.prototype.getDataByAjax = function (url, methodParam) {
            var _this = this;
            var d = $.Deferred();
            $.ajax({
                url: url,
                type: "POST",
                data: methodParam,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: true,
                processData: false,
                cache: false,
                success: function (msg) {
                    var records;
                    //input data to source object
                    if (msg.d != null) {
                        records = msg.d;
                    }
                    else if (msg.data != null) {
                        records = msg.data;
                    }
                    else {
                        records = msg;
                    }
                    d.resolve(records);
                },
                error: function (err) {
                    if (_this.source.options.show_error != null && _this.source.options.show_error) {
                        alert("Unavailable, " + "Error Msg : " + err);
                    }
                    if (_this.source.options.show_debug) {
                        console.log("Ajax Data Loaded Failed !!! => " + err);
                    }
                    var e = {
                        status: "error"
                    };
                    var objs = [];
                    objs.push(e);
                    d.fail(objs);
                },
                complete: function (msg) {
                    d.always();
                }
            });
            return d;
        };
        //public
        Grid.prototype.importData = function (records, callbackDone) {
            var _this = this;
            $(document).ready(function () {
                _this.source.data = records;
                _this.loadingRowsAtStart();
                //this.reloadRows();
                if (_this.source.loadingCounter == 0) {
                    _this.onFirstTimeRecordsLoaded();
                }
                _this.onRecordsLoaded(_this.source.loadingCounter + 1);
                _this.source.loadingCounter++;
                if (_this.source.options.show_debug) {
                    console.log("Ajax Data Loaded Success => " + _this.source.data.length + " rows");
                }
                if (callbackDone != null) {
                    var e = {
                        status: "success"
                    };
                    callbackDone(e);
                }
            });
        };
        //public
        Grid.prototype.catchData = function (selectUrl, selectUrlParams, callbackDone) {
            var _this = this;
            if (selectUrl != null) {
                this.source.options.select_url = selectUrl;
                if (selectUrlParams != null) {
                    this.source.options.select_url_params = selectUrlParams;
                }
            }
            //fill data when html loaded completely
            $(document).ready(function () {
                //show loader
                _this.showLoader(true);
                if (_this.source.options.show_debug) {
                    console.log("Catching Data => url : " + _this.source.options.select_url);
                }
                //get record from server (web service or web api)
                _this.selectData(null, callbackDone);
                if (_this.source.options.show_debug) {
                    console.log("Catching Data Completed.");
                }
            });
        };
        Grid.prototype.showLoader = function (visible) {
            var loaderId = this.gridId + "_loader_row";
            //remove loader element if existed
            if ($("#" + this.gridId + " #" + loaderId).length > 0) {
                $("#" + this.gridId + " #" + loaderId).remove();
            }
            //add loader element
            if (visible) {
                var textMsg = "";
                if (this.source.options.lang == null || this.source.options.lang == farsiLang) {
                    textMsg = "در حال بارگذاری، لطفاً صبور باشید . . .";
                }
                else if (this.source.options.lang == englishLang) {
                    textMsg = "Please Wait . . .";
                }
                else if (this.source.options.lang == turkLang) {
                    textMsg = "Lütfen bekle . . .";
                }
                $("#" + this.gridId + " > table > tbody").append("<tr class='" + radynLoaderClassName + "' id='" + loaderId + "' style='text-align:center;'><td style='padding:20px 0 20px 0;' colspan='100%'>" + textMsg + "</td></tr>");
            }
        };
        //public
        // get grid id by one elemnt of grid table
        Grid.prototype.getGridId = function (htmlElement) {
            if (htmlElement == null) {
                return null;
            }
            var parentDiv = $(htmlElement).parents("." + rootGridClassName + "");
            var id = parentDiv[0].id;
            return id;
        };
        //--- Prepare Base Html Tags ----------------------------
        Grid.prototype.loadHtmlGrid = function () {
            //load previous options from cookie
            if (this.source.options.state == null || this.source.options.state) {
                //load state
                this.loadtState();
                if (this.source.options.show_debug) {
                    console.log("State Loaded");
                }
            }
            //load default data as Current Data
            //this.currentData = this.source.data;
            this.source.currentData = jQuery.extend(true, [], this.source.data);
            //create structure elements of table 
            this.prepareTableStructure();
            if (this.source.options.show_debug) {
                console.log("Table Structure Created");
            }
            //fill header data
            this.fillTableHeader();
            if (this.source.options.show_debug) {
                console.log("Table Header Created");
            }
            //fill footer data of table
            this.fillTableFooter();
            if (this.source.options.show_debug) {
                console.log("Table Footer Created");
            }
            //create html elements for show row numbering
            this.prepareTableInfo();
            if (this.source.options.show_debug) {
                console.log("Table Info Created");
            }
            //create html elements of filter textbox
            this.prepareFiltering();
            if (this.source.options.show_debug) {
                console.log("Table Filtering Created");
            }
            //create html elements of search textbox
            this.prepareSearching();
            if (this.source.options.show_debug) {
                console.log("Table Searching Created");
            }
            //loading record data to table & load page navigation & load tabel info text
            //this.loadingRowsAtStart();
            //initilize event
            this.initializeEvents();
            if (this.source.options.show_debug) {
                console.log("Table Events Fired");
            }
        };
        Grid.prototype.prepareSearching = function () {
            // if Filtering Enabled
            if (this.source.options.searching) {
                var searchTitle = "";
                if (this.source.options.lang == null || this.source.options.lang == farsiLang) {
                    searchTitle = "جستجو";
                }
                else if (this.source.options.lang == englishLang) {
                    searchTitle = "search";
                }
                else if (this.source.options.lang == turkLang) {
                    searchTitle = "ara";
                }
                var filteringId = "filter_" + this.gridId;
                //if does not exist create new 
                if ($("#" + this.gridId + " " + "." + filteringRecordClassName).length == 0) {
                    $('<div class="' + filteringRecordClassName + '" id="' + filteringId + '"></div>').insertBefore("#tbl_" + this.gridId);
                }
                var htmlText = "";
                htmlText += '<div class="col-lg-3 col-sm-12">';
                htmlText += '<label class="floating-label" for="float-Search">' + searchTitle + '</label>';
                htmlText += '<input class="form-control ' + searchInputClassName + '" type="text" value="" id="float-Search" placeholder="' + searchTitle + '...">';
                htmlText += '</div>';
                // Add to Html
                $("#" + filteringId).html(htmlText);
            }
        };
        Grid.prototype.prepareFiltering = function () {
            // if Filtering Enabled
            if (this.source.options.filtering) {
                var filterTitle = "";
                if (this.source.options.lang == null || this.source.options.lang == farsiLang) {
                    filterTitle = "جستجو";
                }
                else if (this.source.options.lang == englishLang) {
                    filterTitle = "search";
                }
                else if (this.source.options.lang == turkLang) {
                    filterTitle = "ara";
                }
                //var filteringId = "filter_row_" + id;
                var htmlText = "";
                htmlText += "<tr>";
                for (var j = 0; j < this.source.options.columns.length; j++) {
                    var colHdr = this.source.options.columns[j];
                    var tdClassNames = "";
                    if (colHdr.visible == false) {
                        tdClassNames += " " + invisibleColumnClassName;
                    }
                    htmlText += '<td class="' + tdClassNames + '" >';
                    if (colHdr.filtering == null || colHdr.filtering == true) {
                        var dataBoundText = (colHdr.name != null) ? dataBoundAttrib + '="' + colHdr.name + '"' : "";
                        //start div dropdown
                        htmlText += '<div>';
                        htmlText += '<ul class="margin-no nav nav-list pull-right" >';
                        htmlText += '<li class="dropdown" >';
                        //start filter type button
                        htmlText += '<a class="dropdown-toggle text-default waves-attach no-pading" data-toggle="dropdown" >';
                        htmlText += ' <span class="fa fa-filter ' + filterTypeButtonClassName + '" ></span>';
                        htmlText += ' </a >';
                        //start ul filter type items
                        htmlText += '<ul class="dropdown-menu">';
                        //start li filter type items
                        htmlText += this.getFilterTypesDropDownList(colHdr);
                        htmlText += '</ul>';
                        htmlText += '</li>';
                        htmlText += '</ul>';
                        //add filtering textbox
                        htmlText += '<div class="overflow">';
                        htmlText += '<input class=" ' + filterInputClassName + ' form-control" ' + dataBoundText + ' type="text" value="" placeholder="' + filterTitle + '...">';
                        htmlText += '</div>';
                        htmlText += '</div>';
                        htmlText += '</td>';
                    }
                }
                htmlText += "</tr>";
                // Add to html
                //$(htmlText).insertBefore("#" + tableName + " > tbody");
                $(htmlText).insertBefore("#" + this.gridId + "> table > tbody");
                //hide the columns with "Visible=false"
                $("#" + this.gridId + " " + "." + invisibleColumnClassName).hide();
                //Sedghi Codes
                //add funtion on filter button
                $("body").click(function (e) {
                    $("." + filterTypeButtonClassName).parent().next().hide(200);
                    if ($(e.target).hasClass(filterTypeButtonClassName) && $(e.target).parent().next().css('display') == 'none') {
                        $(e.target).parent().next().show(200);
                    }
                });
            }
        };
        Grid.prototype.prepareTableInfo = function () {
            // if Filtering Enabled
            if (this.source.options.show_table_info != null && this.source.options.show_table_info) {
                var tableInfoId = "table_info_" + this.gridId;
                //if does not exist create new 
                if ($("#" + this.gridId + " " + "." + tableInfoClassName).length == 0) {
                    $('<div class="' + tableInfoClassName + '" id="' + tableInfoId + '"></div>').insertAfter("#tbl_" + this.gridId);
                }
                // Add to Html
                $("#" + tableInfoId).html("");
            }
        };
        Grid.prototype.preparePaging = function (data) {
            var _this = this;
            if ($("#" + this.gridId + " " + "." + pagenaviSectionClassName) != null) {
                $("#" + this.gridId + " " + ".paginate_buttons").remove();
            }
            if (data == null) {
                return;
            }
            var pagingId = "pagenavi_" + this.gridId;
            //if does not exist create new 
            if ($("#" + this.gridId + " " + "." + pagenaviSectionClassName).length == 0) {
                $('<div class="' + pagenaviSectionClassName + ' card-action" id="' + pagingId + '"></div>').insertAfter("#tbl_" + this.gridId);
            }
            var htmlText = '<ul class="card-action-btn pull-right">';
            if (data.length <= this.source.options.page_post_number) {
                htmlText += '<li class="disabled"  page="' + 1 + '" ><a>' + 1 + '</a></li>';
            }
            else {
                var pageNumberCount = parseInt((data.length / this.source.options.page_post_number).toString());
                var lefroverPages = data.length % this.source.options.page_post_number;
                if (lefroverPages > 0) {
                    pageNumberCount++;
                }
                var nextButtonClassNameTemp = "";
                var prevButtonClassNameTemp = "";
                var firstButtonClassNameTemp = "";
                var lastButtonClassNameTemp = "";
                if (this.source.options.current_page == pageNumberCount) {
                    nextButtonClassNameTemp += " " + "disabled";
                    lastButtonClassNameTemp += " " + "disabled";
                }
                if (this.source.options.current_page == 1) {
                    prevButtonClassNameTemp += " " + "disabled";
                    firstButtonClassNameTemp += " " + "disabled";
                }
                var startPageNumber = 1;
                var endPageNumber = pageNumberCount;
                if (this.source.options.max_page_number >= 0) {
                    if (pageNumberCount > this.source.options.max_page_number) {
                        var _val = this.source.options.max_page_number / 2;
                        var startRangePageNumber = _val;
                        var endRangePageNumber = startRangePageNumber - 1;
                        if (this.source.options.current_page > startRangePageNumber && (pageNumberCount - this.source.options.current_page) > endRangePageNumber) {
                            startPageNumber = this.source.options.current_page - startRangePageNumber;
                            endPageNumber = this.source.options.current_page + endRangePageNumber;
                        }
                        else if (this.source.options.current_page > startRangePageNumber && (pageNumberCount - this.source.options.current_page) <= endRangePageNumber) {
                            endPageNumber = this.source.options.current_page + endRangePageNumber;
                            if (endPageNumber > pageNumberCount) {
                                endPageNumber = pageNumberCount;
                            }
                            //startPageNumber = this.source.options.current_page - startRangePageNumber;
                            startPageNumber = pageNumberCount - (startRangePageNumber + endRangePageNumber);
                        }
                        else {
                            endPageNumber = this.source.options.max_page_number;
                        }
                    }
                }
                var firstTitle = "";
                var previousTitle = "";
                var nextTitle = "";
                var lastTitle = "";
                if (this.source.options.lang == null || this.source.options.lang == farsiLang) {
                    firstTitle = "اول";
                    lastTitle = "آخر";
                    nextTitle = "بعدی";
                    previousTitle = "قبلی";
                }
                else if (this.source.options.lang == englishLang) {
                    firstTitle = "First";
                    lastTitle = "Last";
                    nextTitle = "Next";
                    previousTitle = "Prev";
                }
                else if (this.source.options.lang == turkLang) {
                    firstTitle = "ilk";
                    lastTitle = "son";
                    nextTitle = "Sonraki";
                    previousTitle = "önceki";
                }
                //first page
                htmlText += '<li class="' + pageButtonClassName + ' ' + firstButtonClassName + ' ' + firstButtonClassNameTemp + '" ' + pageIndexAttrib + '="first"><a>' + firstTitle + '</a></li>';
                //Previous
                htmlText += '<li class="' + pageButtonClassName + ' ' + prevButtonClassName + ' ' + prevButtonClassNameTemp + '" ' + pageIndexAttrib + '="prev"><a>' + previousTitle + '</a></li>';
                // improve start page number for more beauty
                if (startPageNumber > 1) {
                    startPageNumber++;
                }
                // improve end page number for more beauty
                if (endPageNumber < pageNumberCount) {
                    endPageNumber--;
                }
                // add ... at frist
                if (startPageNumber > 1) {
                    htmlText += '<li class="disabled"><a>' + "..." + '</a></li>';
                }
                for (var i = startPageNumber; i <= endPageNumber; i++) {
                    var classNames = "";
                    if (i == this.source.options.current_page) {
                        classNames += " " + currentPageClassName;
                        classNames += " " + "active ";
                    }
                    if (i == pageNumberCount) {
                        classNames += " " + lastPageButtonClassName;
                    }
                    //page number
                    htmlText += '<li class="' + pageButtonClassName + ' ' + classNames + '" ' + pageIndexAttrib + '="' + i + '" ><a>' + i + '</a></li>';
                }
                // add ... to end
                if (endPageNumber < pageNumberCount) {
                    htmlText += '<li class="disabled" ><a>' + "..." + '</a></li>';
                }
                //next
                htmlText += '<li class="' + pageButtonClassName + ' ' + nextButtonClassName + ' ' + nextButtonClassNameTemp + '" ' + pageIndexAttrib + '="next"><a>' + nextTitle + '</a></li>';
                //last page
                htmlText += '<li class="' + pageButtonClassName + ' ' + lastButtonClassName + ' ' + lastButtonClassNameTemp + '" ' + pageIndexAttrib + '="' + pageNumberCount + '"><a>' + lastTitle + '</a></li>';
                htmlText += '</ul>';
                //disable useless page buttons
                if (this.source.options.current_page == 1) {
                }
            }
            //append html to Source Html Code
            $("#" + pagingId).html(htmlText);
            //event of click on page navigation
            //this event most created after create html elements (page navigation elements)
            $("#" + this.gridId + " " + "." + pageButtonClassName).click(function (e) {
                var self = $(e.currentTarget);
                var page = self.attr(pageIndexAttrib); //load custom attribute ("pg-idx")
                if (page == null) {
                    return;
                }
                if (page == "first") {
                    if (_this.source.options.current_page != 1) {
                        _this.setPageNumber(1);
                    }
                }
                else if (page == "last") {
                    var lastpageNum = 10000;
                    var lastPageElm = $("#" + _this.gridId + " " + "." + lastPageButtonClassName);
                    if (lastPageElm != null && lastPageElm.attr(pageIndexAttrib) != null) {
                        lastpageNum = Number(lastPageElm.attr(pageIndexAttrib)); //load custom attribute ("pg-idx")
                    }
                    if (_this.source.options.current_page < lastpageNum) {
                        _this.setPageNumber(lastpageNum);
                    }
                }
                else if (page == "next") {
                    var lastpageNum = 10000;
                    var lastPageElm = $("#" + _this.gridId + " " + "." + lastPageButtonClassName);
                    if (lastPageElm != null && lastPageElm.attr(pageIndexAttrib) != null) {
                        lastpageNum = Number(lastPageElm.attr(pageIndexAttrib)); //load custom attribute ("pg-idx")
                    }
                    if (_this.source.options.current_page < lastpageNum) {
                        _this.source.options.current_page++;
                    }
                }
                else if (page == "prev") {
                    if (_this.source.options.current_page > 1) {
                        _this.source.options.current_page--;
                    }
                }
                else {
                    _this.source.options.current_page = Number(page);
                }
                _this.fillRows(_this.source.currentData);
            });
        };
        Grid.prototype.prepareTableStructure = function () {
            //add root grid class Name to Main div
            $("#" + this.gridId).addClass(rootGridClassName);
            this.clearGridHtml();
            var tableId = "tbl_" + this.gridId;
            var htmlText = "";
            //start Table
            htmlText += " <table id='" + tableId + "' class='table table-hover table-mc table-bordered table-condensed' dir='rtl'> ";
            if (this.source.options.show_table_header == null || this.source.options.show_table_header) {
                //start & end Table Header
                htmlText += "<thead><tr></tr></thead>";
            }
            //start & end Table Body
            htmlText += "  <tbody></tbody>";
            if (this.source.options.show_table_footer != null && this.source.options.show_table_footer) {
                //start & end Table Footer
                htmlText += "  <tfoot><tr></tr></tfoot> ";
            }
            //end Table
            htmlText += "  </table> ";
            //append to Html Source Code
            if ($("#" + this.gridId).length > 0) {
                $("#" + this.gridId).append(htmlText);
            }
        };
        //--- initialize CallBack -------------------------------
        Grid.prototype.initializeEvents = function () {
            var _this = this;
            //Event - fire when Serach text on textbox changed
            if ($("#" + this.gridId + " " + "." + searchInputClassName) != null) {
                $("#" + this.gridId + " " + "." + searchInputClassName).bind("input", function (e) {
                    var self = $(e.currentTarget);
                    var searchText = self.val();
                    if (searchText != "") {
                        //reset current page number on searching
                        if (_this.source.options.filter_reset_paging) {
                            _this.setPageNumber(1);
                        }
                    }
                    //disable filtering text input when searching
                    if (_this.source.options.reset_filtering_on_searching) {
                        _this.clearFiltering();
                    }
                    _this.searchGrid(searchText);
                });
            }
            //Event - fire When Text of Filter textbox changed for each Column
            if ($("#" + this.gridId + " " + "." + filterInputClassName) != null) {
                $("#" + this.gridId + " " + "." + filterInputClassName).bind("input", function (e) {
                    var self = $(e.currentTarget);
                    //get data bound item from elemnt
                    var dataBoundName = self.attr(dataBoundAttrib);
                    var filterText = self.val();
                    //change filtertype in column of source[id]
                    //this.setSourceColumnData(dataBoundName, [{ "FilterText": filterText }]);
                    var input = new GridColumn();
                    input.filter_text = filterText;
                    _this.setSourceColumnData(dataBoundName, input);
                    _this.showSearchAndFilterResult();
                });
            }
            //Event - fire when Filter Type Change for Each Column
            if ($("#" + this.gridId + " " + "." + filterTypeItemClassName) != null) {
                $("#" + this.gridId + " " + "." + filterTypeItemClassName).bind("click", function (e) {
                    var self = $(e.currentTarget);
                    //If Item Disabled
                    var isDisabled = self.hasClass("disabled");
                    if (isDisabled) {
                        return;
                    }
                    //get parent element
                    var parent = self.parent();
                    //remove "active" class from all li elemnts
                    $(parent).find("li").removeClass("active");
                    //add "active" class to current element
                    self.addClass("active");
                    //get data bound item from elemnt
                    var dataBoundName = self.attr(dataBoundAttrib);
                    //get filter type from elemet
                    var newFilterType = self.attr(filterTypeAttrib);
                    //change filtertype in column of source[id]
                    //this.setSourceColumnData(dataBoundName, [{ "FilterType": newFilterType }]);
                    var input = new GridColumn();
                    input.filter_type = newFilterType;
                    _this.setSourceColumnData(dataBoundName, input);
                    //check if text value existed in filter input box
                    var elementObj = _this.getElementObjectOfFilterInput(dataBoundName);
                    if (elementObj != null && elementObj.length > 0) {
                        var textSearch = elementObj.val();
                        if (textSearch == null || textSearch == "") {
                            return;
                        }
                    }
                    _this.showSearchAndFilterResult();
                });
            }
            //Event  - fire when Leave Page Event
            window.onbeforeunload = function () {
                //var message = "Your confirmation message goes here.";
                //e = e || window.event;
                //// For IE and Firefox
                //if (e) {
                //    e.returnValue = message;
                //}
                //// For Safari
                //return message;
                _this.saveState();
            };
        };
        Grid.prototype.loadingRowsAtStart = function () {
            //show search and filter result 
            this.showSearchAndFilterResult();
        };
        //--- Save & Load State ----------------------------------
        Grid.prototype.clearGridHtml = function () {
            $("#" + this.gridId).html(""); //tbody inside #TableName
            //$("#" + gridId + " > table > tbody").html(""); //tbody inside #TableName
            if ($("#" + this.gridId + " " + "." + pagenaviSectionClassName) != null) {
                $("#" + this.gridId + " " + "." + pagenaviSectionClassName).html("");
            }
            if ($("#" + this.gridId + " " + "." + tableInfoClassName) != null) {
                $("#" + this.gridId + " " + "." + tableInfoClassName).html("");
            }
        };
        //saving some optiong in cookies
        Grid.prototype.saveState = function () {
            if (this.source.options.current_page != null) {
                this.setCookie(this.gridId + "_" + 'current_page', this.source.options.current_page, 3);
            }
            if (this.source.options.page_post_number != null) {
                this.setCookie(this.gridId + "_" + 'page_post_number', this.source.options.page_post_number, 3);
            }
            if (this.source.options.current_sort_column != null) {
                this.setCookie(this.gridId + "_" + 'current_sort_column', this.source.options.current_sort_column, 3);
            }
            var srcCol = this.getSourceColumn(this.source.options.current_sort_column);
            if (srcCol != null) {
                //this.setCookie(this.gridId + "_" + 'current_sort_order', this.source.options.current_sort_order, 3);    
                this.setCookie(this.gridId + "_" + 'current_sort_order', srcCol.sort_order, 3);
            }
            //Search Box
            if (this.source.options.search_text != null && this.source.options.search_text != "") {
                this.setCookie(this.gridId + "_" + 'search_text', this.source.options.search_text, 3);
            }
            else {
                this.setCookie(this.gridId + "_" + 'search_text', "", 3);
            }
            //Filter Boxes
            for (var i = 0; i < this.source.options.columns.length; i++) {
                var col = this.source.options.columns[i];
                var cNameFtxt = this.gridId + "_" + "ftxt_" + col.name;
                if (col.filter_text != null && col.filter_text != "" && col.filter_text != "undefined") {
                    this.setCookie(cNameFtxt, col.filter_text, 3);
                }
                else {
                    this.deleteCookie(cNameFtxt);
                }
                var cNameFtyp = this.gridId + "_" + "ftyp_" + col.name;
                if (col.filter_type != null && col.filter_type != "" && col.filter_type != "undefined") {
                    this.setCookie(cNameFtyp, col.filter_type, 3);
                }
                else {
                    this.deleteCookie(cNameFtyp);
                }
            }
        };
        //loading some optiong from cookies
        Grid.prototype.loadtState = function () {
            if (this.checkCookie(this.gridId + "_" + 'current_page')) {
                this.source.options.current_page = Number(this.getCookie(this.gridId + "_" + 'current_page'));
            }
            if (this.checkCookie(this.gridId + "_" + 'current_sort_column')) {
                this.source.options.current_sort_column = this.getCookie(this.gridId + "_" + 'current_sort_column');
            }
            if (this.checkCookie(this.gridId + "_" + 'current_sort_order')) {
                //this.source.options.current_sort_order = this.getCookie(this.gridId + "_" + 'current_sort_order');
                var srcCol = this.getSourceColumn(this.source.options.current_sort_column);
                if (srcCol != null) {
                    srcCol.sort_order = this.getCookie(this.gridId + "_" + 'current_sort_order');
                }
            }
            if (this.checkCookie(this.gridId + "_" + 'page_post_number')) {
                this.source.options.page_post_number = Number(this.getCookie(this.gridId + "_" + 'page_post_number'));
            }
            //Search Box
            if (this.checkCookie(this.gridId + "_" + 'search_text')) {
                this.source.options.search_text = this.getCookie(this.gridId + "_" + 'search_text');
            }
            //Filter Boxes
            for (var j = 0; j < this.source.options.columns.length; j++) {
                var col = this.source.options.columns[j];
                var ftxt = this.gridId + "_" + "ftxt_" + col.name;
                var ftyp = this.gridId + "_" + "ftyp_" + col.name;
                if (this.checkCookie(ftxt)) {
                    var filterText = this.getCookie(ftxt);
                    if (filterText != null && filterText != "" && filterText != "undefined") {
                        col.filter_text = filterText;
                    }
                    var filterType = this.getCookie(ftyp);
                    if (filterType != null && filterType != "" && filterType != "undefined") {
                        col.filter_type = filterType;
                    }
                }
            }
        };
        //public
        Grid.prototype.saveStateData = function () {
            this.saveState();
        };
        //public
        Grid.prototype.loadStateData = function () {
            this.loadtState();
            this.refreshRows();
        };
        //public
        Grid.prototype.deleteStateData = function () {
            this.deleteCookie(this.gridId + "_" + 'current_page');
            this.deleteCookie(this.gridId + "_" + 'current_sort_column');
            this.deleteCookie(this.gridId + "_" + 'current_sort_order');
            this.deleteCookie(this.gridId + "_" + 'page_post_number');
            this.deleteCookie(this.gridId + "_" + 'search_text');
            //Filter Boxes
            for (var j = 0; j < this.source.options.columns.length; j++) {
                var col = this.source.options.columns[j];
                var ftxt = this.gridId + "_" + "ftxt_" + col.name;
                var ftyp = this.gridId + "_" + "ftyp_" + col.name;
                this.deleteCookie(ftxt);
                this.deleteCookie(ftyp);
            }
        };
        //public
        Grid.prototype.setStateMode = function (mode) {
            if (mode == false)
                this.source.options.state = false;
            else {
                this.source.options.state = true;
            }
        };
        //--- Editing ---------------------------------------------
        Grid.prototype.editRow = function (keyCols) {
            this.getTrElementByKey(keyCols);
        };
        Grid.prototype.editRowByTrElement = function (trElement) {
            /*
            $(trElement).find("td").each((tdIdx) => {
                var tdElm = $(trElement).find("td").get(tdIdx);
                var cmd = $(tdElm).attr(dataCommandAttrib);
                var dtBoundName = $(tdElm).attr(dataBoundAttrib);
                if (cmd != null && cmd == "edit") {
                    var htmlText = "";
                    htmlText += '<a class="btn btn-primary btn-md">ذخیره</a>';
                    htmlText += "<br />";
                    htmlText += '<a class="btn btn-default btn-md">لغو</a>';
                    $(tdElm).html(htmlText);
                } else if ($(tdElm).hasClass(isKeyColumnClassName) == true) {

                } else if (dtBoundName != null) {
                    var col = this.getSourceColumn(dtBoundName);
                    //if was editable
                    if (col.Editable == null || col.Editable == true) {
                        var innerText = tdElm.innerText;
                        var htmlText = '<input type="text" value="' + innerText + '" />';
                        $(tdElm).html(htmlText);
                    }
                }
            });
            */
        };
        //--- Sorting ----------------------------------------------
        //sort table rows
        Grid.prototype.sortRows = function (data) {
            var _this = this;
            //check sort data
            //if (this.source.options.current_sort_order != null && this.source.options.current_sort_column != null) {
            if (this.source.options.current_sort_column != null) {
                var colName = this.source.options.current_sort_column;
                //var sortOrder = this.source.options.current_sort_order;
                var sortOrder = "ASC";
                var col = this.getSourceColumn(colName);
                if (col != null) {
                    if (col.sort_order != null) {
                        sortOrder = col.sort_order;
                    }
                }
                else if (this.source.options.current_sort_order != null) {
                    sortOrder = this.source.options.current_sort_order;
                }
                if (data == null || data.length == null || data.length == 0) {
                    return;
                }
                if (sortOrder == "desc") {
                    //sort DESC
                    data.sort(function (recordA, recordB) {
                        var itemValueA = _this.getSourceValue(colName, recordA);
                        var itemValueB = _this.getSourceValue(colName, recordB);
                        if (itemValueA == null) {
                            itemValueA = "";
                        }
                        if (itemValueB == null) {
                            itemValueB = "";
                        }
                        //sorting for not null result
                        if (typeof itemValueA == "string") {
                            return -(itemValueA.toString().localeCompare(itemValueB.toString()));
                        }
                        else if (typeof itemValueA == "number") {
                            return Number(itemValueB) - Number(itemValueA);
                        }
                    });
                }
                else {
                    //sort ASC
                    data.sort(function (recordA, recordB) {
                        var itemValueA = _this.getSourceValue(colName, recordA);
                        var itemValueB = _this.getSourceValue(colName, recordB);
                        if (itemValueA == null) {
                            itemValueA = "";
                        }
                        if (itemValueB == null) {
                            itemValueB = "";
                        }
                        //if was null sorted in end of results
                        if (itemValueA.toString().length == 0 && itemValueB.toString().length == 0) {
                            return 1;
                        }
                        else if (itemValueA.toString().length == 0 && itemValueB.toString().length != 0) {
                            return 1;
                        }
                        else if (itemValueA.toString().length != 0 && itemValueB.toString().length == 0) {
                            return -1;
                        }
                        //sorting for not null result
                        if (typeof itemValueA == "string") {
                            return itemValueA.toString().localeCompare(itemValueB.toString());
                        }
                        else if (typeof itemValueA == "number") {
                            return Number(itemValueA) - Number(itemValueB);
                        }
                    });
                }
            }
        };
        //public
        Grid.prototype.sortTable = function (colName, sortOrder) {
            //set input data to options
            this.source.options.current_sort_column = colName;
            //this.source.options.current_sort_order = sortOrder;
            var srcCol = this.getSourceColumn(colName);
            if (srcCol != null) {
                srcCol.sort_order = sortOrder;
            }
            //sort rows
            this.sortRows(this.source.currentData);
            //refresh data for show results
            this.refreshRows();
        };
        //--- Column ------------------------------------------------
        //checking of input column is key
        Grid.prototype.isColmunKey = function (columnName) {
            //error Handling
            if (this.source.options.key_columns == null) {
                return false;
            }
            //search in table key columns
            for (var i = 0; i < this.source.options.key_columns.length; i++) {
                var keyCol = this.source.options.key_columns[i];
                if (columnName != null && columnName.toLowerCase() == keyCol.toLowerCase()) {
                    return true;
                }
            }
            return false;
        };
        Grid.prototype.haveColumnBinding = function (colObject) {
            if (colObject.name != null) {
                return true;
            }
            else if (colObject.html != null) {
                var columnBinding = this.splitTemplateField(colObject.html);
                if (columnBinding != null && columnBinding.length > 0) {
                    return true;
                }
            }
            return false;
        };
        //return source columne data from "options.columns" by column name
        Grid.prototype.getSourceColumn = function (name) {
            for (var i = 0; i < this.source.options.columns.length; i++) {
                var colHdr = this.source.options.columns[i];
                if (colHdr.name == name) {
                    return colHdr;
                }
            }
        };
        //set data to source columne data (options.columns)
        Grid.prototype.setSourceColumnData = function (name, newSettings) {
            var col = this.getSourceColumn(name);
            if (col == null) {
                return;
            }
            if (newSettings.filter_text != null) {
                col["filter_text"] = newSettings.filter_text;
            }
            if (newSettings.filter_type != null) {
                col["FilterType"] = newSettings.filter_type;
            }
            if (newSettings.visible != null) {
                col["visible"] = newSettings.visible;
            }
            if (newSettings.caption != null) {
                col["caption"] = newSettings.caption;
            }
            if (newSettings.sorting != null) {
                col["sorting"] = newSettings.sorting;
            }
            //if (newSettings.Command != null) {
            //    col["Command"] = newSettings.Command;
            //}
            if (newSettings.html != null) {
                col["html"] = newSettings.html;
            }
            if (newSettings.filtering != null) {
                col["filtering"] = newSettings.filtering;
            }
        };
        //--- Key Columns Unique Text -------------------------------
        //combine and return unique text from key columns for using as tr id with keyCol input
        Grid.prototype.getUniqueTextByKey = function (keyData) {
            var rowObject = this.getSourceRecordByKey(keyData);
            return this.getUniqueText(rowObject);
        };
        //combine and return unique text from key columns for using as tr id
        Grid.prototype.getUniqueText = function (rowObject) {
            //error handling
            if (rowObject == null) {
                return null;
            }
            //error handling
            if (this.source.options.key_columns == null) {
                return null;
            }
            //declare variables
            var keyValuesText = "";
            for (var i = 0; i < this.source.options.key_columns.length; i++) {
                var keyCol = this.source.options.key_columns[i];
                keyValuesText += "_";
                keyValuesText += rowObject[keyCol];
            }
            return keyValuesText;
        };
        //combine and return Key Columns text from source record object
        Grid.prototype.getKeyDataText = function (rowObject) {
            if (this.source.options.key_columns == null) {
                return null;
            }
            if (rowObject == null) {
                return null;
            }
            var keyColsText = "";
            keyColsText += '{';
            for (var i = 0; i < this.source.options.key_columns.length; i++) {
                var keyCol = this.source.options.key_columns[i];
                if (i > 0) {
                    keyColsText += ",";
                }
                if (typeof rowObject[keyCol] == "number") {
                    keyColsText += '\"' + keyCol + '\": ' + rowObject[keyCol] + '';
                }
                else {
                    keyColsText += '\"' + keyCol + '\":\"' + rowObject[keyCol] + '\"';
                }
            }
            keyColsText += '}';
            return keyColsText;
        };
        //--- Search & Filtering -------------------------------------
        //split data with filtring and searching together
        Grid.prototype.showSearchAndFilterResult = function () {
            var _this = this;
            var promis = this.getSearchResult();
            promis.done(function (res) {
                //searching data
                var searchResult = res;
                //filtering data
                searchResult = _this.getFilterRows(searchResult);
                //reset paging
                _this.resetPaging();
                //sort rows
                _this.sortRows(searchResult);
                //fill rows
                _this.fillRows(searchResult);
            });
        };
        Grid.prototype.getSearchResult = function () {
            var _this = this;
            var d = $.Deferred();
            if (this.source.options.ajax_searching) {
                clearTimeout(this.currentAjaxTimeout);
                this.currentAjaxTimeout = setTimeout(function () {
                    if (_this.source.options.search_text != null && _this.source.options.search_text != "") {
                        var promise = _this.getDataByAjax(_this.source.options.search_url, '{' + searchAjaxParameterName + ' : "' + _this.source.options.search_text + '"}');
                        promise.done(function (res) {
                            d.resolve(res);
                        });
                    }
                    else {
                        d.resolve(_this.source.data);
                    }
                }, this.source.options.ajax_searching_delay);
            }
            else {
                var searchResult = this.getSearchRows(this.source.data);
                d.resolve(searchResult);
            }
            return d;
        };
        Grid.prototype.searchGrid = function (searchText) {
            //convert null to empty
            if (searchText == null) {
                searchText = "";
            }
            //apply search on grid
            this.source.options.search_text = searchText;
            //
            this.showSearchAndFilterResult();
        };
        Grid.prototype.clearFiltering = function () {
            //clear all filter textbox and clear all FilterText variable in  this.source.options
            $("#" + this.gridId + " " + "." + filterInputClassName).val("");
            for (var i = 0; i < this.source.options.columns.length; i++) {
                var col = this.source.options.columns[i];
                col.filter_text = null;
            }
        };
        //disable all filtering textbox
        Grid.prototype.disableFiltering = function () {
            $("#" + this.gridId + " " + "." + filterInputClassName).attr("readonly", "true");
        };
        //enable all filtering textbox
        Grid.prototype.enableFiltering = function () {
            $("#" + this.gridId + " " + "." + filterInputClassName).attr("readonly", "false");
        };
        Grid.prototype.filterRows = function (colObject, filterText, filterType, data) {
            var _this = this;
            switch (filterType) {
                case "equal":
                    return data.filter(function (row) {
                        var itemValue = _this.getContentValue(colObject, row);
                        if (itemValue != null) {
                            if (itemValue.toString().toLowerCase() == filterText.toString().toLowerCase()) {
                                return true;
                            }
                        }
                        return false;
                    });
                case "not_equal":
                    return data.filter(function (row) {
                        var itemValue = _this.getContentValue(colObject, row);
                        if (itemValue != null) {
                            if (itemValue.toString().toLowerCase() != filterText.toString().toLowerCase()) {
                                return true;
                            }
                        }
                        return false;
                    });
                case "bigger_than":
                    return data.filter(function (row) {
                        var itemValue = _this.getContentValue(colObject, row);
                        if (itemValue != null) {
                            if (typeof itemValue == "number") {
                                if (Number(itemValue) > Number(filterText)) {
                                    return true;
                                }
                            }
                        }
                        return false;
                    });
                case "smaller_than":
                    return data.filter(function (row) {
                        var itemValue = _this.getContentValue(colObject, row);
                        if (itemValue != null) {
                            if (typeof itemValue == "number") {
                                if (Number(itemValue) < Number(filterText)) {
                                    return true;
                                }
                            }
                        }
                        return false;
                    });
                case "contain":
                    return data.filter(function (row) {
                        var itemValue = _this.getContentValue(colObject, row);
                        if (itemValue != null) {
                            //var result = itemValue.toString().toLowerCase().includes(filterText.toLowerCase());
                            //var result = this.includes(itemValue.toString().toLowerCase(), filterText.toLowerCase());
                            var result = _this.contains(itemValue.toString().toLowerCase(), filterText.toLowerCase());
                            if (result == true) {
                                return true;
                            }
                        }
                        return false;
                    });
                case "start_with":
                    return data.filter(function (row) {
                        var itemValue = _this.getContentValue(colObject, row);
                        if (itemValue != null) {
                            //var result = itemValue.toString().toLowerCase().startsWith(filterText.toLowerCase());
                            var result = _this.startsWith(itemValue.toString().toLowerCase(), filterText.toLowerCase());
                            if (result == true) {
                                return true;
                            }
                        }
                        return false;
                    });
                case "end_with":
                    return data.filter(function (row) {
                        var itemValue = _this.getContentValue(colObject, row);
                        if (itemValue != null) {
                            //var result = itemValue.toString().toLowerCase().endsWith(filterText.toLowerCase());
                            var result = _this.endsWith(itemValue.toString().toLowerCase(), filterText.toLowerCase());
                            if (result == true) {
                                return true;
                            }
                        }
                        return false;
                    });
            }
        };
        Grid.prototype.compareFiltering = function (recordObj, colNames, filterText) {
            for (var i = 0; i < colNames.length; i++) {
                var col = colNames[i];
                var itemValue = this.getSourceValue(col, recordObj);
                if (itemValue != null) {
                    var result = itemValue.toString().toLowerCase().indexOf(filterText.toLowerCase());
                    if (result >= 0) {
                        return true;
                    }
                }
            }
        };
        Grid.prototype.getFilterRows = function (data) {
            if (this.source.options.filtering) {
                //at the first set all input data as filter result
                var filterResult = data;
                for (var j = 0; j < this.source.options.columns.length; j++) {
                    var col = this.source.options.columns[j];
                    //fill filter text box with FilterText of "Source[].options.columns"
                    //var filterBox = $("#" + this.gridId + " " + "." + filterInputClassName + "[" + dataBoundAttrib + "=\"" + col.Name + "\"]");
                    var filterBox = this.getElementObjectOfFilterInput(col.name);
                    if (filterBox != null) {
                        if (col.filter_text != null && col.filter_text != "" && col.filter_text != "undefined") {
                            $(filterBox).val(col.filter_text);
                        }
                    }
                    //filtering data if FilterText was not empty or null
                    if (col.filter_text != null && col.filter_text != "") {
                        //filter "filterResult" data and set on "filterResult"
                        filterResult = this.filterRows(col, col.filter_text, col.filter_type, filterResult);
                    }
                }
                return filterResult;
            }
            return data;
        };
        //get the filter input box Jquery Object
        Grid.prototype.getElementObjectOfFilterInput = function (colName) {
            return $("#" + this.gridId + " " + "." + filterInputClassName + "[" + dataBoundAttrib + "=\"" + colName + "\"]");
        };
        //searching data
        Grid.prototype.getSearchRows = function (data) {
            var _this = this;
            //if (this.source.options.searching) {
            //fill search textbox with SearchText from "source[].options.columns"
            if ($("#" + this.gridId + " " + "." + searchInputClassName) != null) {
                var searchBox = $("#" + this.gridId + " " + "." + searchInputClassName);
                if (this.source.options.search_text == null || this.source.options.search_text == "") {
                    $(searchBox).val("");
                }
                else {
                    $(searchBox).val(this.source.options.search_text);
                }
            }
            //return current data if search text was empty or null
            if (this.source.options.search_text == null || this.source.options.search_text == "") {
                return data;
            }
            else {
                var searchableColumns = this.getSearchableColumns();
                var searchResult = data.filter(function (recordObj) {
                    //if want search in specific columns
                    //if (this.source.options.search_columns != null && this.source.options.search_columns.length != 0) {
                    if (searchableColumns != null && searchableColumns.length != 0) {
                        return _this.compareFiltering(recordObj, searchableColumns, _this.source.options.search_text); //(OR Results for Combine Results)
                    }
                    else {
                        //var cols = this.source.options.columns.extractVariableAsArray("Name");
                        for (var i = 0; i < _this.source.options.columns.length; i++) {
                            var col = _this.source.options.columns[i];
                            var contentValue = _this.getContentValue(col, recordObj);
                            if (contentValue != null) {
                                var result = contentValue.toString().toLowerCase().indexOf(_this.source.options.search_text.toLowerCase());
                                if (result >= 0) {
                                    return true;
                                }
                            }
                        }
                    }
                });
                return searchResult;
            }
            //}
            return data;
        };
        Grid.prototype.getSearchableColumns = function () {
            var result = [];
            for (var i = 0; i < this.source.options.columns.length; i++) {
                var col = this.source.options.columns[i];
                if (col.searching) {
                    result.push(col.name);
                }
            }
            return result;
        };
        //return filter type drop down list html
        Grid.prototype.getFilterTypesDropDownList = function (colHdr) {
            if (colHdr == null) {
                return "";
            }
            var defaultCompareItem = "contain";
            var compareList = [];
            if (this.source.options.lang == null || this.source.options.lang == farsiLang) {
                compareList = [
                    { Name: "equal", Title: "برابر باشد با" },
                    { Name: "not_equal", Title: "برابر نباشد با" },
                    { Name: "bigger_than", Title: "بزرگتر از" },
                    { Name: "smaller_than", Title: "کوچکتر از" },
                    { Name: "contain", Title: "شامل عبارت" },
                    { Name: "start_with", Title: "شروع شود با" },
                    { Name: "end_with", Title: "پایان یابد با" },
                ];
            }
            else if (this.source.options.lang == englishLang) {
                compareList = [
                    { Name: "equal", Title: "Equal" },
                    { Name: "not_equal", Title: "Not Equal" },
                    { Name: "bigger_than", Title: "Bigger than" },
                    { Name: "smaller_than", Title: "Smaller than" },
                    { Name: "contain", Title: "Contain" },
                    { Name: "start_with", Title: "Start with" },
                    { Name: "end_with", Title: "End with" },
                ];
            }
            if (colHdr.filter_type == null) {
                colHdr.filter_type = defaultCompareItem;
            }
            //var dataBoundText = (colHdr.Name != null) ? dataBoundAttrib + '="' + colHdr.Name + '"' : "";
            var htmlText = "";
            for (var i = 0; i < compareList.length; i++) {
                var compareItem = compareList[i];
                var attributes = new HtmlAttributes();
                if (colHdr.name != null) {
                    attributes.add(dataBoundAttrib, colHdr.name, true);
                }
                //var classNames = "";
                attributes.addWithMerge("class", filterTypeItemClassName, " ");
                if (colHdr.filter_type != null && colHdr.filter_type.toLowerCase() == compareItem.Name.toLowerCase()) {
                    //classNames += " active";
                    attributes.addWithMerge("class", "active", " ");
                }
                else {
                    if (colHdr.filter_type_changing == false) {
                        //classNames += " disabled";
                        attributes.addWithMerge("class", "disabled", " ");
                    }
                }
                //var filterTypeText = filterTypeAttrib + "=\"" + compareItem.Name + "\"";
                attributes.add(filterTypeAttrib, compareItem.Name, true);
                //htmlText += "<li class=\"" + filterTypeItemClassName + " " + classNames + "\" " + dataBoundText + " " + filterTypeText + ">";
                htmlText += "<li " + attributes.getAsHtmlAttributes() + " >";
                htmlText += "<a>";
                htmlText += compareItem.Title;
                htmlText += "</a>";
                htmlText += "</li>";
            }
            return htmlText;
        };
        //public 
        Grid.prototype.setSearchText = function (searchText) {
            this.searchGrid(searchText);
        };
        //public
        Grid.prototype.clearSearchAndFilter = function () {
            this.source.options.search_text = "";
            this.clearFiltering();
            this.source.currentData = this.source.data;
            this.refreshRows();
        };
        //--- Fill ------------------------------------------------------
        Grid.prototype.fillTableHeader = function () {
            var _this = this;
            //reset data
            $("#" + this.gridId + " > table > thead > tr").html("");
            //
            for (var j = 0; j < this.source.options.columns.length; j++) {
                var col = this.source.options.columns[j];
                var attributes = new HtmlAttributes();
                //var classNames = "";
                //classNames = columnHeaderClassName;
                attributes.addWithMerge("class", columnHeaderClassName, " ");
                if (col.sorting || col.sorting == null) {
                    attributes.addWithMerge("class", sortableColumnsClassName, " ");
                }
                if (col.visible == false) {
                    attributes.addWithMerge("class", invisibleColumnClassName, " ");
                }
                if (col.name != null && col.name == this.source.options.current_sort_column) {
                    attributes.addWithMerge("class", sortedColumnClassName, " ");
                    //classNames += " " + sortedColumnClassName;
                    //if (this.source.options.current_sort_order == "asc") {
                    if (col.sort_order != null && col.sort_order.toLowerCase() == "asc") {
                        attributes.addWithMerge("class", sortOrderAscClassName, " ");
                    }
                    else {
                        attributes.addWithMerge("class", sortOrderDescClassName, " ");
                    }
                }
                else {
                    attributes.addWithMerge("class", unsortedColumnClassName, " ");
                }
                if (col.width != null && col.width.length > 0) {
                    attributes.add("width", col.width, true);
                }
                //var dataBoundText = (col.Name != null) ? dataBoundAttrib + '="' + col.Name + '"' : "";
                if (col.name != null) {
                    attributes.addWithMerge(dataBoundAttrib, col.name, " ");
                }
                if (col.header_attributes != null) {
                    attributes.addRangeAsMerge(col.header_attributes, " ");
                }
                var caption = "";
                if (col.caption != null) {
                    caption = col.caption;
                }
                //var dataCommandText = (col.Command != null) ? dataCommandAttrib + '="' + col.Command + '"' : "";
                //var dataCommandText = "";
                $("#" + this.gridId + " > table > thead > tr").append(//tr inside thead inside #tableName
                //"<th class=\"" + classNames + "\" " + dataBoundText + "  " + dataCommandText + "  id=\"" + columnHeaderIdNamePerfix + col.Name + "\">" + col.Caption + "</th>"
                "<th " + attributes.getAsHtmlAttributes() + " id=\"" + columnHeaderIdNamePerfix + col.name + "\">" +
                    caption
                    + "</th>");
            }
            //hide invisible table header
            $("#" + this.gridId + " " + "." + invisibleColumnClassName).hide();
            //Event - Sorting Table when click on header columns
            $("#" + this.gridId + " " + "." + sortableColumnsClassName).click(function (e) {
                var self = $(e.currentTarget);
                var selectedSortCol = self.attr(dataBoundAttrib); //load custom attribute ("page")
                var oldSortCol = _this.source.options.current_sort_column;
                _this.source.options.current_sort_column = selectedSortCol;
                var srcCol = _this.getSourceColumn(selectedSortCol);
                if (oldSortCol == selectedSortCol) {
                    if (srcCol.sort_order == "asc") {
                        srcCol.sort_order = "desc";
                    }
                    else {
                        srcCol.sort_order = "asc";
                    }
                }
                else {
                    //this.source.options.current_sort_order = "asc";
                    srcCol.sort_order = "asc";
                }
                if (_this.source.options.reset_paging_on_sorting == null || _this.source.options.reset_paging_on_sorting == true) {
                    _this.setPageNumber(1);
                }
                _this.sortRows(_this.source.currentData);
                _this.fillRows(_this.source.currentData);
                _this.fillTableHeader();
            });
        };
        Grid.prototype.fillTableFooter = function () {
            //reset data
            $("#" + this.gridId + "> table > tfoot > tr").html("");
            for (var j = 0; j < this.source.options.columns.length; j++) {
                var col = this.source.options.columns[j];
                var attributes = new HtmlAttributes();
                //var classNames = "";
                //classNames += columnFooterClassName;
                attributes.addWithMerge("class", columnFooterClassName, " ");
                //if column was visible
                if (col.visible == false) {
                    //classNames += " " + invisibleColumnClassName;
                    attributes.addWithMerge("class", invisibleColumnClassName, " ");
                }
                if (col.footer_attributes != null) {
                    attributes.addRangeAsMerge(col.footer_attributes, " ");
                }
                if (col.width != null && col.width.length > 0) {
                    attributes.add("width", col.width, true);
                }
                var caption = "";
                if (col.caption != null) {
                    caption = col.caption;
                }
                //append data to column data
                $("#" + this.gridId + " > table > tfoot > tr").append(//tr inside thead inside #tableName
                //'<th class="' + classNames + '"  id="' + columnFooterIdNamePerfix + col.Name + '">' + col.Caption + "</th>"
                '<th ' + attributes.getAsHtmlAttributes() + '  id="' + columnFooterIdNamePerfix + col.name + '">' +
                    caption
                    + "</th>");
            }
            //hide invisible table footer
            $("#" + this.gridId + " " + "." + invisibleColumnClassName).hide();
        };
        Grid.prototype.clearHtmlRows = function () {
            //clear all rows
            $("#" + this.gridId + " > table > tbody").html("");
        };
        Grid.prototype.fillTableInfo = function () {
            //declare vaiables
            var tblInfoId = "table_info_" + this.gridId;
            //reset table info
            $("#" + tblInfoId).html("");
            var startRecord = (this.source.options.current_page - 1) * this.source.options.page_post_number;
            var endRecord = startRecord + Number(this.source.options.page_post_number);
            //check start number of data
            if (this.source.currentData.length != 0) {
                startRecord++;
            }
            //check end number of rows
            if (this.source.currentData.length < endRecord) {
                endRecord = this.source.currentData.length;
            }
            var htmlText = "";
            if (this.source.options.lang == null || this.source.options.lang == farsiLang) {
                htmlText += "نمایش ردیف های " + (startRecord) + " تا " + endRecord + " از مجموعاً " + this.source.currentData.length + " ردیف";
            }
            else if (this.source.options.lang == englishLang) {
                htmlText += "show records " + (startRecord) + " to " + endRecord + " of total " + this.source.currentData.length + " rows";
            }
            else if (this.source.options.lang == turkLang) {
                htmlText += "toplam " + this.source.currentData.length + " satırın " + startRecord + " ile " + endRecord+" arası kayıtları göster";
            }
            //show when filtering or searching data
            if (this.source.data != null && this.source.currentData.length != this.source.data.length) {
                if (this.source.options.lang == null || this.source.options.lang == farsiLang) {
                    htmlText += " (فیلتر شده از " + this.source.data.length + " ردیف)";
                }
                else if (this.source.options.lang == englishLang) {
                    htmlText += " (filtered from " + this.source.data.length + " rows)";
                }
                else if (this.source.options.lang == turkLang) {
                    htmlText += " (" + this.source.data.length +" satırdan filtrelendi";
                }
            }
            //append data to html
            $("#" + tblInfoId).html(htmlText);
        };
        Grid.prototype.fillRows = function (data) {
            var _this = this;
            if (this.source.options.show_debug) {
                console.log("Start Filling Rows.");
            }
            var noRecordText = "";
            if (this.source.options.lang == null || this.source.options.lang == farsiLang) {
                noRecordText = "رکوردی وجود ندارد";
            }
            else if (this.source.options.lang == englishLang) {
                noRecordText = "no records";
            }
            else if (this.source.options.lang == turkLang) {
                noRecordText = "kayıt yok";
            }
            //deep copy of 'data'
            this.source.currentData = jQuery.extend(true, [], data);
            //ریست کردن و پاک کردن رکوردها
            this.clearHtmlRows();
            //No Records
            if (data == null || data.length == 0) {
                $("#" + this.gridId + " > table > tbody").append("<tr bgColor='White'><td colspan='100%'>" + noRecordText + "</td></tr>");
            }
            else {
                //reset "current_page" if data length less than page_post_number
                if (data.length < this.source.options.page_post_number) {
                    this.setPageNumber(1);
                }
                //determine starting row number & ending row number
                var startRowNumber = 0;
                var endRowNumber = data.length;
                if (this.source.options.paging) {
                    startRowNumber = (this.source.options.current_page - 1) * this.source.options.page_post_number;
                    endRowNumber = startRowNumber + this.source.options.page_post_number;
                    if (endRowNumber > data.length) {
                        endRowNumber = data.length;
                    }
                }
                //var rowNum = 1;
                //show data in tables
                for (var i = startRowNumber; i < endRowNumber; i++) {
                    var rowObject = data[i];
                    var trElementText = this.getHtmlRow(rowObject, i);
                    //convert to jQuery Element Object
                    var trElementObj = $(trElementText);
                    //callback
                    this.onRowHtmlInserting(trElementObj, i, rowObject);
                    //add to html source
                    $("#" + this.gridId + " > table > tbody").append(trElementObj);
                    //callback
                    this.onRowHtmlInserted(trElementObj, i, rowObject);
                }
            }
            //event
            this.onDataRefreshed();
            //if paging was enabled
            if (this.source.options.paging) {
                //preparing pageing
                this.preparePaging(data);
            }
            //show table info
            if (this.source.options.show_table_info) {
                this.fillTableInfo();
            }
            //-- Callbacks ------------------
            //
            $("#" + this.gridId + " " + "." + invisibleColumnClassName).hide();
            /*
            //event for databound attrib
            $("[" + dataCommandAttrib + "]").click((clickEvent) => {
                var self = $(clickEvent.currentTarget);
                //get tr element object
                var trElementObject = self.parents("tr:first").eq(0);
                var rowNumber = this.getRowNumberByTrElement(trElementObject);
                var keyData = this.getKeysByTrElement(trElementObject);
                var commandText = self.attr(dataCommandAttrib);

                //callback
                this.onCommandRunning(clickEvent, trElementObject, keyData, rowNumber, commandText);
            });
            */
            //event for databound attrib
            $("[" + gridCommandAttrib + "]").click(function (clickEvent) {
                var self = $(clickEvent.currentTarget);
                //get tr element object
                var trElementObject = self.parents("tr:first").eq(0);
                var rowNumber = _this.getRowNumberByTrElement(trElementObject);
                var keyData = _this.getKeysByTrElement(trElementObject);
                var commandText = self.attr(gridCommandAttrib);
                //callback
                _this.onCommandRunning(clickEvent, trElementObject, keyData, rowNumber, commandText);
            });
            //Table Row Click Events
            if ($("#" + this.gridId + " " + "." + tableRowClassName) != null) {
                $("#" + this.gridId + " " + "." + tableRowClassName).bind("click", function (clickEvent) {
                    var self = $(clickEvent.currentTarget);
                    //get tr element object
                    var rowNumber = _this.getRowNumberByTrElement(self);
                    var keyData = _this.getKeysByTrElement(self);
                    //event
                    _this.onRowClicked(clickEvent, self, keyData, rowNumber);
                });
            }
            if (this.source.options.show_debug) {
                console.log("End Of Filling Rows.");
            }
        };
        Grid.prototype.getHtmlRow = function (rowObject, rowNum) {
            //declare tr variables
            var trAttributes = new HtmlAttributes();
            //var trClassNames = "";
            //var keyValuesText = "";
            //var rowNumberText = "";
            var rowId = trElementIdNamePerfix + this.getUniqueText(rowObject);
            //keyValuesText = keysAttrib + "='" + this.getKeyDataText(rowObject) + "'";
            trAttributes.add(keysAttrib, this.getKeyDataText(rowObject), true, true);
            //add odd and even class tp tr element
            if ((rowNum % 2) == 1) {
                //trClassNames += " " + "odd";
                trAttributes.addWithMerge("class", "odd", " ");
            }
            else {
                //trClassNames += " " + "even";
                trAttributes.addWithMerge("class", "even", " ");
            }
            //rowNumberText = rowNumberAttrib + "=" + '"' + rowNum++ + '"';
            trAttributes.add(rowNumberAttrib, (rowNum++).toString(), true);
            //trClassNames += " " + tableRowClassName;
            trAttributes.addWithMerge("class", tableRowClassName, " ");
            //create tr element
            //var htmlRowText = "<tr " + rowNumberText + "  " + keyValuesText + " id='" + rowId + "'  class='" + trClassNames + "'>";
            var htmlRowText = "<tr id='" + rowId + "'  " + trAttributes.getAsHtmlAttributes() + " >";
            //create td elements
            for (var k = 0; k < this.source.options.columns.length; k++) {
                //declare td variables
                var col = this.source.options.columns[k];
                var tdAttributes = new HtmlAttributes();
                //var tdClassNames = "";
                //var commandAttribText = "";
                //var dataBindAttribText = "";
                if (col.visible == false) {
                    //tdClassNames += " " + invisibleColumnClassName;
                    tdAttributes.addWithMerge("class", invisibleColumnClassName, " ");
                }
                if (this.isColmunKey(col.name)) {
                    //tdClassNames += " " + isKeyColumnClassName;
                    tdAttributes.addWithMerge("class", isKeyColumnClassName, " ");
                }
                //if (col.Command != null) {
                //    commandAttribText += dataCommandAttrib + ' = "' + col.Command.toLowerCase() + '"';
                //}
                if (col.name != null) {
                    //dataBindAttribText += dataBoundAttrib + ' = "' + col.Name + '"';
                    tdAttributes.addWithMerge(dataBoundAttrib, col.name, " ");
                }
                if (col.width != null && col.width.length) {
                    tdAttributes.add("Width", col.width, true);
                }
                if (col.cell_attributes != null) {
                    tdAttributes.addRangeAsMerge(col.cell_attributes, " ");
                }
                //
                var htmlColText = "";
                //bind field
                if (col.name != null) {
                    //create td element
                    //htmlColText += '<td class=" ' + tdClassNames + '" ' + commandAttribText + ' ' + dataBindAttribText + '>';
                    htmlColText += '<td ' + tdAttributes.getAsHtmlAttributes() + '>';
                    var valueText = this.getCellValue(col.name, rowObject);
                    htmlColText += valueText;
                    htmlColText += "</td>";
                    // add td data to tr data
                    htmlRowText += htmlColText;
                }
                else {
                    var templateField = col.html;
                    if (templateField != null) {
                        //var tags = splitTemplateField(templateField);
                        var bindFields = this.extractBindFieldFromTemplateField(templateField);
                        if (bindFields != null) {
                            for (var i = 0; i < bindFields.length; i++) {
                                var colName = bindFields[i];
                                var cellValue = this.getCellValue(colName, rowObject);
                                templateField = templateField.replace("<" + templateFieldHtmlTag + ">" + colName.trim() + "</" + templateFieldHtmlTag + ">", cellValue);
                            }
                        }
                    }
                    //create td element
                    //htmlColText += '<td class=" ' + tdClassNames + '" ' + commandAttribText + '>';
                    htmlColText += '<td ' + tdAttributes.getAsHtmlAttributes() + '>';
                    htmlColText += templateField;
                    htmlColText += "</td>";
                    // add td data to tr data
                    htmlRowText += htmlColText;
                }
            }
            //close tr element
            htmlRowText += "</tr>";
            return htmlRowText;
        };
        Grid.prototype.splitTemplateField = function (templateField) {
            //get matching data with regular expression
            var tags = templateField.match(/<\s*radyn[^>]*>(.*?)<\s*\/\s*radyn>/g);
            return tags;
        };
        Grid.prototype.extractBindFieldFromTemplateField = function (templateField) {
            var resultArr = [];
            var tags = this.splitTemplateField(templateField);
            if (tags != null) {
                for (var i = 0; i < tags.length; i++) {
                    var tag = tags[i];
                    var colName = this.findStringBetween(tag, '<' + templateFieldHtmlTag + '>', '</' + templateFieldHtmlTag + '>');
                    resultArr.push(colName);
                }
            }
            return resultArr;
        };
        //--- Paging --------------------------------------------
        Grid.prototype.setPageNumber = function (pageNumber) {
            this.source.options.current_page = pageNumber;
        };
        //public
        Grid.prototype.showPage = function (pageNumber) {
            if (pageNumber == null) {
                return;
            }
            var lastpageNum = 100000;
            var lastPageElm = $("#" + this.gridId + " " + "." + lastPageButtonClassName);
            if (lastPageElm != null && lastPageElm.attr(pageIndexAttrib) != null) {
                lastpageNum = Number(lastPageElm.attr(pageIndexAttrib)); //load custom attribute ("pg-idx")
            }
            if (pageNumber < 1) {
                return;
            }
            if (pageNumber > lastpageNum) {
                return;
            }
            this.setPageNumber(pageNumber);
            this.refreshRows();
        };
        //public
        Grid.prototype.showLastPage = function () {
            var lastpageNum = 100000;
            var lastPageElm = $("#" + this.gridId + " " + "." + lastPageButtonClassName);
            if (lastPageElm != null && lastPageElm.attr(pageIndexAttrib) != null) {
                lastpageNum = Number(lastPageElm.attr(pageIndexAttrib)); //load custom attribute ("pg-idx")
            }
            this.showPage(lastpageNum);
        };
        //public
        Grid.prototype.showNextPage = function () {
            this.showPage(this.source.options.current_page + 1);
        };
        //public
        Grid.prototype.showPreviousPage = function () {
            this.showPage(this.source.options.current_page - 1);
        };
        //public
        Grid.prototype.resetPaging = function () {
            this.setPageNumber(1);
        };
        //public
        Grid.prototype.setPagePostNumber = function (pageNumber) {
            if (pageNumber == null) {
                return;
            }
            if (pageNumber == 0) {
                pageNumber = 10000;
            }
            this.source.options.page_post_number = pageNumber;
            this.refreshRows();
        };
        //--- Callbacks --------------------------------------------------
        //
        Grid.prototype.onRowInserting = function (operation, keyData, newRowObject) {
            //eventArgs
            var e = {
                gridId: null,
                record: null,
                keyData: null,
                operation: null
            };
            e.gridId = this.gridId;
            e.operation = operation;
            e.keyData = keyData;
            e.record = newRowObject;
            if (this.source.options.callbacks.row_inserting != null) {
                this.source.options.callbacks.row_inserting(e);
            }
        };
        Grid.prototype.onRowInserted = function (rowObject, keyData) {
            //eventArgs
            var e = {
                gridId: null,
                keyData: null,
                record: null
            };
            e.gridId = this.gridId;
            e.keyData = keyData;
            e.record = rowObject;
            if (this.source.options.callbacks.row_inserted != null) {
                this.source.options.callbacks.row_inserted(e);
            }
        };
        Grid.prototype.onRowHtmlInserting = function (trElementObject, rowNumber, rowObject) {
            //eventArgs
            var e = {
                gridId: null,
                record: null,
                rowNumber: null,
                trElement: null
            };
            e.gridId = this.gridId;
            e.trElement = trElementObject;
            e.record = rowObject;
            e.rowNumber = rowNumber;
            if (this.source.options.callbacks.row_html_inserting != null) {
                this.source.options.callbacks.row_html_inserting(e);
            }
        };
        Grid.prototype.onRowHtmlInserted = function (trElementObject, rowNumber, rowObject) {
            //eventArgs
            var e = {
                gridId: null,
                record: null,
                rowNumber: null,
                trElement: null
            };
            e.gridId = this.gridId;
            e.trElement = trElementObject;
            e.rowNumber = rowNumber;
            e.record = rowObject;
            if (this.source.options.callbacks.row_html_inserted != null) {
                this.source.options.callbacks.row_html_inserted(e);
            }
        };
        Grid.prototype.onRowUpdating = function (operation, keyCols, newRow, oldRow) {
            //eventArgs
            var e = {
                gridId: null,
                keyData: null,
                operation: null,
                newRecord: null,
                oldRecord: null
            };
            e.gridId = this.gridId;
            e.keyData = keyCols;
            e.newRecord = newRow;
            e.oldRecord = oldRow;
            e.operation = operation;
            if (this.source.options.callbacks.row_updating != null) {
                this.source.options.callbacks.row_updating(e);
            }
        };
        Grid.prototype.onRowUpdated = function (keyCols, newRow, oldRow) {
            //eventArgs
            var e = {
                gridId: null,
                keyData: null,
                newRecord: null,
                oldRecord: null
            };
            e.gridId = this.gridId;
            e.keyData = keyCols;
            e.newRecord = newRow;
            e.oldRecord = oldRow;
            if (this.source.options.callbacks.row_updated != null) {
                this.source.options.callbacks.row_updated(e);
            }
        };
        Grid.prototype.onRowRemoving = function (operation, keyCols, sourceIndex, rowObject) {
            //eventArgs
            var e = {
                gridId: null,
                record: null,
                keyData: null,
                operation: null,
                indexRecordOfSource: null
            };
            e.gridId = this.gridId;
            e.keyData = keyCols;
            e.record = rowObject;
            e.indexRecordOfSource = sourceIndex;
            e.operation = operation;
            if (this.source.options.callbacks.row_removing != null) {
                this.source.options.callbacks.row_removing(e);
            }
        };
        Grid.prototype.onRowRemoved = function (keyCols, rowObjectDeleted) {
            //eventArgs
            var e = {
                gridId: null,
                keyData: null,
                deletedRecord: null
            };
            e.gridId = this.gridId;
            e.keyData = keyCols;
            e.deletedRecord = rowObjectDeleted;
            if (this.source.options.callbacks.row_removed != null) {
                this.source.options.callbacks.row_removed(e);
            }
        };
        Grid.prototype.onRowClicked = function (clickData, trElementObject, keyCols, rowNumber) {
            //eventArgs
            var e = {
                gridId: null,
                keyData: null,
                rowNumber: null,
                trElement: null,
                clickData: null,
            };
            e.gridId = this.gridId;
            e.trElement = trElementObject;
            e.keyData = keyCols;
            e.rowNumber = rowNumber;
            e.clickData = clickData;
            if (this.source.options.callbacks.row_clicked != null) {
                this.source.options.callbacks.row_clicked(e);
            }
        };
        Grid.prototype.onCommandRunning = function (clickData, trElementObject, keyData, rowNumber, commandText) {
            //eventArgs
            var e = {
                gridId: null,
                keyData: null,
                rowNumber: null,
                trElement: null,
                clickData: null,
                commandText: null,
            };
            e.gridId = this.gridId;
            e.trElement = trElementObject;
            e.keyData = keyData;
            e.rowNumber = rowNumber;
            e.commandText = commandText;
            e.clickData = clickData;
            if (this.source.options.callbacks.command_running != null) {
                this.source.options.callbacks.command_running(e);
            }
        };
        Grid.prototype.onFirstTimeRecordsLoaded = function () {
            var e = { gridId: null };
            e.gridId = this.gridId;
            if (this.source.options.callbacks.firsttime_data_loaded != null) {
                this.source.options.callbacks.firsttime_data_loaded(e);
            }
        };
        Grid.prototype.onRecordsLoaded = function (recordLoadedCount) {
            var e = {
                recordLoadedCount: null,
                gridId: null
            };
            e.gridId = this.gridId;
            e.recordLoadedCount = recordLoadedCount;
            if (this.source.options.callbacks.data_loaded != null) {
                this.source.options.callbacks.data_loaded(e);
            }
        };
        Grid.prototype.onDataRefreshed = function () {
            var e = { gridId: null };
            e.gridId = this.gridId;
            if (this.source.options.callbacks.data_refreshed != null) {
                this.source.options.callbacks.data_refreshed(e);
            }
        };
        Grid.prototype.onRowHtmlRemoved = function (parameters) {
        };
        Grid.prototype.onRowClicking = function (parameters) {
        };
        Grid.prototype.onBeforeEdit = function (parameters) {
        };
        Grid.prototype.onAfterEdit = function (parameters) {
        };
        Grid.prototype.onCellClicking = function (parameters) {
        };
        Grid.prototype.onCellClicked = function (parameters) {
        };
        Grid.prototype.onRowMouseEvents = function (parameters) {
        };
        Grid.prototype.onCellMouseEvents = function (parameters) {
        };
        //--- Rows ----------------------------------------------------------
        //public
        Grid.prototype.reloadRows = function () {
            //reset page to 1
            this.resetPaging();
            //clear search & filter text data
            this.clearSearchAndFilter();
            //refresh rows to show new rows
            this.refreshRows();
        };
        //public
        Grid.prototype.refreshRows = function () {
            //fill current rows again
            this.fillRows(this.source.currentData);
        };
        //--- //--- Get Row Number -----------------------------------------
        //public
        Grid.prototype.getRowNumberByTrElement = function (trElement) {
            try {
                var rowNumber = Number($(trElement).attr(rowNumberAttrib));
                return rowNumber;
            }
            catch (exp) {
                return null;
            }
        };
        //public
        Grid.prototype.getRowNumberByKey = function (keyData) {
            var rowObj = this.getSourceRecordByKey(keyData);
            if (rowObj != null) {
                var trElement = this.getTrElementByObject(rowObj);
                if (trElement != null) {
                    return this.getRowNumberByTrElement(trElement);
                }
            }
            return null;
        };
        //--- //--- Get Key Columns ----------------------------------------
        //public
        Grid.prototype.getKeysByNumber = function (rowNum) {
            var tr = $("#" + this.gridId + " " + "." + tableRowClassName).filter("[" + rowNumberAttrib + "='" + rowNum + "']");
            var keysString = $(tr).attr(keysAttrib);
            if (keysString != null) {
                var findItem = null;
                var keyVals = JSON.parse(keysString);
                return keyVals;
            }
            return null;
        };
        //public
        Grid.prototype.getKeysByTrElement = function (trElement) {
            var keysString = $(trElement).attr(keysAttrib);
            if (keysString != null) {
                var keyVals = JSON.parse(keysString);
                return keyVals;
            }
            return null;
        };
        Grid.prototype.exportKeysFromObject = function (rowObject) {
            //error handling
            if (rowObject == null) {
                return null;
            }
            //error handling
            if (this.source.options.key_columns == null) {
                return null;
            }
            //declare variable 
            var res = {};
            for (var j = 0; j < this.source.options.key_columns.length; j++) {
                var keyCol = this.source.options.key_columns[j];
                res[keyCol] = rowObject[keyCol];
            }
            return res;
        };
        //--- //--- Get Row Index --------------------------------------------
        Grid.prototype.getIndexRecord = function (data, rowObject) {
            var idx = $.inArray(rowObject, data);
            return idx;
        };
        //get source index
        Grid.prototype.getSourceRecordIndexByKey = function (keyData) {
            var item = this.getSourceRecordByKey(keyData);
            if (item == null) {
                return -1;
            }
            return this.getIndexRecord(this.source.data, item);
        };
        //get source record
        Grid.prototype.getSourceRecordIndexByNumber = function (rowNum) {
            var keyVals = this.getKeysByNumber(rowNum);
            return this.getSourceRecordIndexByKey(keyVals);
        };
        //--- //--- Get Row Object [Source Record] ----------------------------
        Grid.prototype.getRecordByKey = function (data, keyData) {
            var _this = this;
            if (keyData == null) {
                return null;
            }
            if (this.source.options.key_columns == null) {
                return null;
            }
            var findItem = data.filter(function (item) {
                for (var i = 0; i < _this.source.options.key_columns.length; i++) {
                    var keyCol = _this.source.options.key_columns[i];
                    if (item[keyCol] != keyData[keyCol]) {
                        return false;
                    }
                    return true;
                }
                return false;
            });
            if (findItem != null && findItem.length > 0) {
                return findItem[0];
            }
            return null;
        };
        //public
        //get all source records
        Grid.prototype.getSourceRecords = function () {
            return this.source.data;
        };
        //public
        //celar all source records
        Grid.prototype.clearSourceData = function () {
            //
            this.source.data = [];
            this.source.currentData = [];
            this.refreshRows();
        };
        //public
        //get source record
        Grid.prototype.getSourceRecordByNumber = function (rowNum) {
            var keyVals = this.getKeysByNumber(rowNum);
            return this.getSourceRecordByKey(keyVals);
        };
        //public
        //get source record
        Grid.prototype.getSourceRecordByKey = function (keyData) {
            if (keyData == null) {
                return null;
            }
            return this.getRecordByKey(this.source.data, keyData);
        };
        //public
        //get cell value
        Grid.prototype.getSourceValueByKey = function (keyData, yourColName) {
            if (keyData == null) {
                return null;
            }
            if (yourColName == null) {
                return null;
            }
            var datRecord = this.getRecordByKey(this.source.data, keyData);
            if (datRecord == null) {
                return null;
            }
            return datRecord[yourColName];
        };
        //get the value of bind field
        Grid.prototype.getSourceValue = function (colName, rowObject) {
            //error handeling
            if (colName == null) {
                return null;
            }
            //error handeling
            if (rowObject == null) {
                return null;
            }
            //set source to result
            var result = rowObject;
            try {
                //split column names with dot
                var splitNames = colName.split('.');
                // get value step by step
                for (var i = 0; i < splitNames.length; i++) {
                    var n = splitNames[i];
                    //if child of object was null return null
                    if (result[n] == null) {
                        result = null;
                        break;
                    }
                    // fill "result" with "child of result"
                    result = result[n];
                }
            }
            catch (e) {
                throw e;
                return null;
            }
            if (result == null) {
                return null;
            }
            return result;
        };
        //get the value of bind field and template field
        Grid.prototype.getContentValue = function (colObject, rowObject) {
            //error handeling
            if (colObject == null) {
                return null;
            }
            //error handeling
            if (rowObject == null) {
                return null;
            }
            var content;
            if (colObject.name != null) {
                //content = rowObject[colObject.Name];
                content = this.getSourceValue(colObject.name, rowObject);
            }
            else {
                content = colObject.html;
                if (this.haveColumnBinding(colObject)) {
                    var bindFields = this.extractBindFieldFromTemplateField(colObject.html);
                    if (bindFields != null) {
                        for (var i = 0; i < bindFields.length; i++) {
                            var colName = bindFields[i];
                            var val = this.getSourceValue(colName, rowObject);
                            content = content.replace("<" + templateFieldHtmlTag + ">" + colName.trim() + "</" + templateFieldHtmlTag + ">", val);
                        }
                    }
                }
            }
            //return content;
            var res = this.html2Text(content);
            return res;
        };
        //get the view mode of bind field and template field
        Grid.prototype.getCellValue = function (colName, rowObject) {
            var result = null;
            try {
                result = this.getSourceValue(colName, rowObject);
                //get source column data
                var col = this.getSourceColumn(colName);
                if (col != null) {
                    //if result was null
                    if (result == null) {
                        if (col.null_text != null) {
                            result = col.null_text;
                        }
                        else {
                            result = "";
                        }
                    }
                }
                else {
                    if (result == null) {
                        result = "";
                    }
                }
                //convert digit to number (persin digit to latin && latin digit to persian)
                if (this.source.options.persian_digit != null) {
                    if (this.source.options.persian_digit == true) {
                        result = this.convertEnglishNumberToPersian(result);
                    }
                    else {
                        result = this.convertPersianNumberToEnglish(result);
                    }
                }
            }
            catch (e) {
                result = "Error!!!";
            }
            return result;
        };
        //--- //--- Insert, Update, Delete [In Source] ------------------------------
        Grid.prototype.insertRecordByIndex = function (data, idx, rowObject) {
            data.splice(idx, 0, rowObject); //add item to source as first item
        };
        Grid.prototype.removeRecordByIndex = function (data, idx) {
            data.splice(idx, 1);
        };
        //public
        //insert object to source
        Grid.prototype.insertSourceRecord = function (rowObject, appendToTop) {
            if (rowObject == null) {
                return;
            }
            //callback variables
            var rowInserringOperation = { cancel: null };
            rowInserringOperation.cancel = false;
            var keyData = this.exportKeysFromObject(rowObject);
            //callback
            this.onRowInserting(rowInserringOperation, keyData, rowObject);
            if (appendToTop == null) {
            }
            else if (appendToTop == false) {
                this.source.data.push(rowObject); //add item to source as last item
                this.source.currentData.push(rowObject); //add item to current records as last item
                this.refreshRows();
            }
            else if (appendToTop) {
                this.insertRecordByIndex(this.source.data, 0, rowObject); //add item to source as first item
                this.insertRecordByIndex(this.source.currentData, 0, rowObject); //add item to current records as first item
                this.refreshRows();
            }
            //callback
            this.onRowInserted(rowObject, keyData);
        };
        //public
        //update object in source
        Grid.prototype.updateSourceRecord = function (updatedRow, refreshRecords) {
            if (updatedRow == null) {
                return;
            }
            //get keys column data
            var keyData = this.exportKeysFromObject(updatedRow);
            var oldItemSrc = this.getSourceRecordByKey(keyData);
            //var srcIdx = getIndexRecord(this.source.data, oldItemSrc);
            var oldItemCrnt = this.getRecordByKey(this.source.currentData, keyData);
            //var crntIdx = getIndexRecord(this.currentData, oldItemCrnt);
            //callback variables
            var rowUpdatingOperation = { cancel: null };
            rowUpdatingOperation.cancel = false;
            //callback
            this.onRowUpdating(rowUpdatingOperation, keyData, updatedRow, oldItemSrc);
            //cancel updating
            if (rowUpdatingOperation.cancel) {
                return;
            }
            //appply for source data
            this.applyUpdatedRecordOnSource(oldItemSrc, updatedRow);
            //appply for current data
            this.applyUpdatedRecordOnSource(oldItemCrnt, updatedRow);
            //refresh data
            if (refreshRecords == true) {
                this.refreshRows();
            }
            //callback
            this.onRowUpdated(keyData, updatedRow, oldItemSrc);
        };
        //find source properties and replace with new values
        Grid.prototype.applyUpdatedRecordOnSource = function (srouceRecord, editedRecord) {
            for (var srcProp in srouceRecord) {
                if (this.hasOwnProperty.call(srouceRecord, srcProp)) {
                    for (var editProp in editedRecord) {
                        if (this.hasOwnProperty.call(editedRecord, editProp)) {
                            if (srcProp == editProp) {
                                srouceRecord[srcProp] = editedRecord[editProp];
                                break;
                            }
                        }
                    }
                }
            }
        };
        //public
        //find and return source record and apply new data on it by update record
        Grid.prototype.findAndReplaceNewDataOnSourceRecord = function (updatedRow) {
            try {
                //export key data from updated row
                var keyData = this.exportKeysFromObject(updatedRow);
                //get source record
                var sourceRec = this.getSourceRecordByKey(keyData);
                //deep copy of 'source record'
                var copySourceRec = jQuery.extend(true, {}, sourceRec);
                //apply change on copy source reocrd
                this.applyUpdatedRecordOnSource(copySourceRec, updatedRow);
                return copySourceRec;
            }
            catch (e) {
                return null;
            }
        };
        //public
        //remove object from source
        Grid.prototype.removeSourceRecord = function (keyData, refreshRecords) {
            if (keyData == null) {
                return false;
            }
            //get source index and current index
            var srcIdx = this.getSourceRecordIndexByKey(keyData);
            var currentItem = this.getRecordByKey(this.source.currentData, keyData);
            var crntIdx = this.getIndexRecord(this.source.currentData, currentItem);
            //calback variables
            var rowRemovingOpereation = { cancel: null };
            rowRemovingOpereation.cancel = false;
            //callback
            this.onRowRemoving(rowRemovingOpereation, keyData, srcIdx, currentItem);
            //cancel removing
            if (rowRemovingOpereation.cancel) {
                return false;
            }
            if (srcIdx != null && srcIdx >= 0) {
                //this.source.data.splice(srcIdx, 1);
                this.removeRecordByIndex(this.source.data, srcIdx);
            }
            //remove by Id from current data
            if (crntIdx != null && crntIdx >= 0) {
                this.removeRecordByIndex(this.source.currentData, crntIdx);
            }
            //Refreshing the Rows
            if (refreshRecords == true) {
                this.refreshRows();
            }
            //callback
            this.onRowRemoved(keyData, currentItem);
            return true;
        };
        //rollback source record
        Grid.prototype.rollbackRecord = function (keyData) {
            if (keyData == null) {
                return false;
            }
            var number = this.getRowNumberByKey(keyData);
            var srcRec = this.getSourceRecordByNumber(number);
            if (number == null) {
                return false;
            }
            var htmlTr = this.getHtmlRow(srcRec, number);
        };
        //--- //--- Get Tr Elements Methods -----------------------------
        //public
        Grid.prototype.getTrElementByNumber = function (rowNum) {
            var tr = $("#" + this.gridId + " " + "." + tableRowClassName).filter("[" + rowNumberAttrib + "='" + rowNum + "']");
            return tr;
        };
        Grid.prototype.getTrElementByObject = function (rowObject) {
            var trId = trElementIdNamePerfix + this.getUniqueText(rowObject);
            return $("#" + this.gridId + " " + "#" + trId);
        };
        Grid.prototype.getTrElementByKey = function (keyData) {
            var keyColsText = this.getUniqueTextByKey(keyData);
            var trElement = $("#" + this.gridId + " " + "#" + trElementIdNamePerfix + keyColsText);
            return trElement;
        };
        //--- //--- Class Attributes -------------------------------
        //public
        Grid.prototype.addClassToAllRows = function (className) {
            $("#" + this.gridId + " > table > tbody").find("tr").addClass(className);
        };
        //public
        Grid.prototype.addClassToRow = function (rowNum, className) {
            var trElement = this.getTrElementByNumber(rowNum);
            if (trElement != null) {
                if (trElement.hasClass(className) == false) {
                    trElement.addClass(className);
                }
            }
        };
        //public
        Grid.prototype.removeClassFromRow = function (rowNum, className) {
            var trElement = this.getTrElementByNumber(rowNum);
            if (trElement != null) {
                if (trElement.hasClass(className) == false) {
                    trElement.removeClass(className);
                }
            }
        };
        //public
        Grid.prototype.removeClassFromAllRows = function (className) {
            $("#" + this.gridId + " > table > tbody").find("tr").removeClass(className);
        };
        //public
        Grid.prototype.existClassInRow = function (rowNum, className) {
            var trElement = this.getTrElementByNumber(rowNum);
            if (trElement != null) {
                return trElement.hasClass(className);
            }
            return false;
        };
        //--- Columns --------------------------------------------------
        Grid.prototype.hideColumn = function (columnName) {
        };
        Grid.prototype.showColumn = function (columnName) {
        };
        Grid.prototype.getColumnCaption = function (columnName) {
            var col = this.getSourceColumn(columnName);
            if (col == null || col.caption == null) {
                return null;
            }
            return col.caption;
        };
        //--- Callbacks -------------------------------------------------
        Grid.prototype.setCallback = function (callbackName, func) {
            if (callbackName == null) {
                return;
            }
            this.source.options.callbacks[callbackName.toLowerCase()] = func;
            //if start with callback
            //if (this.startsWith(callbackName, "callback")) {
            //    this.source.options[callbackName] = func;
            //} else { // if not start with callback => append callback_  to cllback Name
            //    this.source.options["callback_" + callbackName] = func;
            //}
        };
        Grid.prototype.setOption = function (optionName, value) {
            this.source.options[optionName] = value;
        };
        //--- Others ----------------------------------------------------
        Grid.prototype.convertEnglishNumberToPersian = function (enDigit) {
            if (enDigit == null) {
                return null;
            }
            var number = enDigit.toString();
            var newValue = "";
            for (var i = 0; i < number.length; i++) {
                var ch = number.charCodeAt(i);
                if (ch >= 48 && ch <= 57) {
                    // european digit range
                    var newChar = ch + 1584;
                    newValue = newValue + String.fromCharCode(newChar);
                }
                else
                    newValue = newValue + String.fromCharCode(ch);
            }
            return newValue;
        };
        Grid.prototype.convertPersianNumberToEnglish = function (enDigit) {
            if (enDigit == null) {
                return null;
            }
            var number = enDigit.toString();
            var newValue = "";
            for (var i = 0; i < number.length; i++) {
                var ch = number.charCodeAt(i);
                if (ch >= 1776 && ch <= 1785) {
                    var newChar = ch - 1728;
                    newValue = newValue + String.fromCharCode(newChar);
                }
                else if (ch >= 1632 && ch <= 1641) {
                    var newChar = ch - 1584;
                    newValue = newValue + String.fromCharCode(newChar);
                }
                else
                    newValue = newValue + String.fromCharCode(ch);
            }
            return newValue;
        };
        Grid.prototype.setCookie = function (cname, cvalue, exSecond) {
            var d = new Date();
            if (exSecond == null) {
                exSecond = 60;
            }
            d.setTime(d.getTime() + (exSecond * 1000));
            var expires = "expires=" + d.toUTCString();
            document.cookie = cname + "=" + cvalue + "; " + expires;
        };
        Grid.prototype.deleteCookie = function (cname) {
            document.cookie = cname + "=;expires=Wed; 01 Jan 1970";
        };
        Grid.prototype.getCookie = function (cname) {
            var name = cname + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ')
                    c = c.substring(1);
                if (c.indexOf(name) == 0)
                    return c.substring(name.length, c.length);
            }
            return "";
        };
        Grid.prototype.checkCookie = function (cname) {
            if (this.getCookie(cname) != null && this.getCookie(cname) != "") {
                return true;
            }
            return false;
        };
        //-----------------
        Grid.prototype.findStringBetween = function (elem, bbTagStart, bbTagClose) {
            var tagString = elem.substring(elem.indexOf(bbTagStart) + bbTagStart.length, elem.indexOf(bbTagClose));
            return tagString.trim();
        };
        Grid.prototype.startsWith = function (baseString, searchString, position) {
            position = position || 0;
            return baseString.indexOf(searchString, position) === position;
        };
        Grid.prototype.endsWith = function (baseString, searchString, position) {
            var subjectString = baseString;
            if (position === undefined || position > subjectString.length) {
                position = subjectString.length;
            }
            position -= searchString.length;
            var lastIndex = subjectString.indexOf(searchString, position);
            return lastIndex !== -1 && lastIndex === position;
        };
        Grid.prototype.includes = function (baseString, argument) {
            'use strict';
            return String.prototype.indexOf.apply(baseString, argument) !== -1;
        };
        Grid.prototype.contains = function (baseString, searchStr, startIndex) {
            return ''.indexOf.call(baseString, searchStr, startIndex) !== -1;
        };
        // converts HTML to text using Javascript
        Grid.prototype.html2Text = function (html) {
            var obj = $("<div>" + html + "</div>");
            var txt = obj.text();
            return txt;
            //var tag = document.createElement('div');
            //tag.innerHTML = html;
            //return tag.innerText;
        };
        return Grid;
    })();
    Radyn.Grid = Grid;
    var HtmlAttributeItem = (function () {
        function HtmlAttributeItem() {
        }
        return HtmlAttributeItem;
    })();
    var HtmlAttributes = (function () {
        function HtmlAttributes() {
            this.source = [];
        }
        HtmlAttributes.prototype.add = function (key, value, overriteIfExist, singleQuote) {
            if (overriteIfExist === void 0) { overriteIfExist = true; }
            if (singleQuote === void 0) { singleQuote = false; }
            var newItem = new HtmlAttributeItem();
            newItem.key = key;
            newItem.value = value;
            newItem.singleQuote = singleQuote;
            this.addToSource(newItem, overriteIfExist);
        };
        HtmlAttributes.prototype.addWithMerge = function (key, value, spliteValue) {
            var newItem = new HtmlAttributeItem();
            newItem.key = key;
            newItem.value = value;
            this.addToSourceWithMerge(newItem, spliteValue);
        };
        HtmlAttributes.prototype.addRangeAsMerge = function (item, spliteValue) {
            try {
                if (item != null) {
                    for (var propKey in item) {
                        if (item.hasOwnProperty(propKey)) {
                            var propValue = item[propKey];
                            this.addWithMerge(propKey, propValue, spliteValue);
                        }
                    }
                }
            }
            catch (ex) { }
        };
        HtmlAttributes.prototype.exist = function (key) {
            var items = this.source.filter(function (item) {
                if (item.key.toLowerCase() == key.toLowerCase()) {
                    return true;
                }
                return false;
            });
            if (items.length > 0) {
                return true;
            }
            else {
                return false;
            }
        };
        HtmlAttributes.prototype.clear = function () {
            this.source = [];
        };
        HtmlAttributes.prototype.get = function (key) {
            var items = this.source.filter(function (item) {
                if (item.key.toLowerCase() == key.toLowerCase()) {
                    return true;
                }
                return false;
            });
            if (items.length > 0) {
                return items[0];
            }
            else {
                return null;
            }
        };
        HtmlAttributes.prototype.remove = function (key) {
            var item = this.get(key);
            if (item != null) {
                var idx = this.getIndexRecord(this.source, item);
                if (idx >= 0) {
                    this.source.splice(idx, 1);
                    return true;
                }
            }
            return false;
        };
        HtmlAttributes.prototype.getAsHtmlAttributes = function () {
            var output = "";
            for (var i = 0; i < this.source.length; i++) {
                var item = this.source[i];
                if (item.singleQuote == false) {
                    output += " " + item.key + "=\"" + item.value + "\" ";
                }
                else {
                    output += " " + item.key + "=\'" + item.value + "\' ";
                }
            }
            return output;
        };
        HtmlAttributes.prototype.getSource = function () {
            return this.source;
        };
        HtmlAttributes.prototype.addToSource = function (item, overriteIfExist) {
            if (overriteIfExist === void 0) { overriteIfExist = true; }
            if (item == null) {
                return;
            }
            var existed = this.exist(item.key);
            if (existed && overriteIfExist) {
                this.remove(item.key);
                this.source.push(item);
            }
            else {
                this.source.push(item);
            }
        };
        HtmlAttributes.prototype.addToSourceWithMerge = function (item, spliteValue) {
            if (item == null) {
                return;
            }
            var existed = this.exist(item.key);
            if (existed == false) {
                this.addToSource(item);
            }
            else {
                var oldItem = this.get(item.key);
                if (oldItem.value.length > 0) {
                    oldItem.value += spliteValue;
                }
                oldItem.value += item.value;
            }
        };
        HtmlAttributes.prototype.getIndexRecord = function (data, rowObject) {
            var idx = $.inArray(rowObject, data);
            return idx;
        };
        return HtmlAttributes;
    })();
})(Radyn || (Radyn = {}));
