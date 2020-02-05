(function($){                                          
$.fn.scrollContent = function(instanceSettings) {
	
	var shortcut =  $.fn.scrollContent;
	
    shortcut.defaultsSettings = {
		scrollType:'button', // button or slider
        btnPrev: null,
        btnNext: null,
        btnSpecific: null,
        auto: null,
        speed: 200,
        easing: null,
        vertical: 0,
        circular: 1,
        visible: 1,
        start: 0,
        scrollAtATime: 1,
        beforeStart: null,
        afterEnd: null
    };
	
	var settings = $.extend({}, $.fn.scrollContent.defaultsSettings , instanceSettings || {});

    return this.each(function() {                           // Returns the element collection. Chainable.

        var running = false;
		var animCss = settings.vertical?"top":"left";
		var sizeCss = settings.vertical?"height":"width";
        var div = $(this);
		var ul = $("ul", div);
		var tLi = $("li", ul);
		var tl = tLi.size();
		var v = settings.visible;

        if(settings.circular) {
            ul.prepend(tLi.slice(tl-v-1+1).clone())
              .append(tLi.slice(0,v).clone());
            settings.start += v;
        }

        var li = $("li", ul);
		var itemLength = li.size();
		var curr = settings.start;
        div.css("visibility", "visible");

        li.css({overflow: "hidden", float: settings.vertical ? "none" : "left"});
        ul.css({margin: "0", padding: "0", position: "relative", "list-style-type": "none", "z-index": "1"});
        div.css({overflow: "hidden", position: "relative", "z-index": "2", left: "0px"});

        var liSize = settings.vertical ? height(li) : width(li);   // Full li size(incl margin)-Used for animation
        var ulSize = liSize * itemLength;                   // size of full ul(total length, not just for the visible items)
        var divSize = liSize * v;                           // size of entire div(total length for just the visible items)

        li.css({width: li.width(), height: li.height()});
        ul.css(sizeCss, ulSize+"px").css(animCss, -(curr*liSize));

        div.css(sizeCss, divSize+"px");                     // Width of the DIV. length of visible images

        if(settings.btnPrev){
            $(settings.btnPrev).click(function() {
                return go(curr-settings.scrollAtATime);
            });
		}

        if(settings.btnNext){
            $(settings.btnNext).click(function() {
                return go(curr+settings.scrollAtATime);
            });
		}

        if(settings.btnSpecific){
            $.each(settings.btnSpecific, function(i, val) {
                $(val).click(function() {
                    return go(settings.circular ? settings.visible+i : i);
                });
            });
		}

        if(settings.auto){
            setInterval(function() {
                go(curr+settings.scrollAtATime);
            }, settings.auto+settings.speed);
		}

        function vis() {
            return li.slice(curr).slice(0,v);
        }

        function go(to) {
            if(!running) {

                if(settings.beforeStart){
                    settings.beforeStart.call(this, vis());
				}
				
                if(settings.circular) {            // If circular we are in first or last, then goto the other end
                    if(to<=settings.start-v-1) {           // If first, then goto last
                        ul.css(animCss, -((itemLength-(v*2))*liSize)+"px");
                        // If "scroll" > 1, then the "to" might not be equal to the condition; it can be lesser depending on the number of elements.
                        curr = to==settings.start-v-1 ? itemLength-(v*2)-1 : itemLength-(v*2)-settings.scrollAtATime;
                    } else if(to>=itemLength-v+1) { // If last, then goto first
                        ul.css(animCss, -( (v) * liSize ) + "px" );
                        // If "scroll" > 1, then the "to" might not be equal to the condition; it can be greater depending on the number of elements.
                        curr = to==itemLength-v+1 ? v+1 : v+settings.scrollAtATime;
                    } else { curr = to;}
                } else {                    // If non-circular and to points to first or last, we just return.
                    if(to<0 || to>itemLength-v){ 
						return;
					}else{ 
						curr = to;
					}
                }                           // If neither overrides it, the curr will still be "to" and we can proceed.

                running = true;

                ul.animate(
                    animCss == "left" ? { left: -(curr*liSize) } : { top: -(curr*liSize) } , settings.speed, settings.easing,
                    function() {
                        if(settings.afterEnd){
                            settings.afterEnd.call(this, vis());
						}
                        running = false;
						
                    }
                );
                // Disable buttons when the carousel reaches the last/first, and enable when not
                if(!settings.circular) {
                    $(settings.btnPrev + "," + settings.btnNext).removeClass("disabled");
                    $( (curr-settings.scrollAtATime<0 && settings.btnPrev)||(curr+settings.scrollAtATime > itemLength-v && settings.btnNext)||[]).addClass("disabled");
                }

            }
            return false;
        }
    });
};

function css(el, prop) {
    return parseInt($.css(el[0], prop)) || 0;
};

function width(el) {
    return  el.outerWidth() + css(el, 'marginLeft') + css(el, 'marginRight');
}
function height(el) {
    return el.outerHeight() + css(el, 'marginTop') + css(el, 'marginBottom');
}

})(jQuery);