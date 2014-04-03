$(function() {
    $("#tabs").tabs();
    $(".button").button();

    $("input:file").change(function() {
        var rel = $(this).attr("rel");
        var photo = getPhoto(rel);
        $(photo).html("<img src='" + $(this).val() + "' class='preview_image' />");
        $("a:has(.ui-icon-arrow-4-diag)[rel=" + rel + "]").trigger("click");
    });

    $("a[href=#]").click(function(e) {
        e.preventDefault();
    });

    $(document)
    .keydown(function(e) {
        // Ctrl
        if (e.keyCode == 17) {
            _longStep = true;
        }
    })
    .keyup(function(e) {
        // Ctrl
        if (e.keyCode == 17) {
            _longStep = false;
        }
    });

    // 放大
    $("a:has(.ui-icon-plus)").click(function(e) {
        var rel = $(this).attr("rel");
        var target = $(".preview_image", getPhoto(rel));
        var width = $(target).width();
        var height = $(target).height();
        var w = width / 100;
        var h = height / 100;
        $(target).width(width + (!_longStep ? w * 10 : w)).height(height + (!_longStep ? h * 10 : h));
    });
    // 缩小
    $("a:has(.ui-icon-minus)").click(function(e) {
        var rel = $(this).attr("rel");
        var target = $(".preview_image", getPhoto(rel));
        var width = $(target).width();
        var height = $(target).height();
        var w = width / 100;
        var h = height / 100;
        $(target).width(width - (!_longStep ? w * 10 : w)).height(height - (!_longStep ? h * 10 : h));
    });

    // 自动调整
    $("a:has(.ui-icon-arrow-4-diag)").click(function(e) {
        var rel = $(this).attr("rel");
        var target = $(".preview_image", getPhoto(rel));
        var parentWidth = $(target).parent().width();
        var parentHeight = $(target).parent().height();
        var width = $(target).width();
        var height = $(target).height();

        $(target).css("left", "0px");
        $(target).css("top", "0px");

        if (parentWidth > parentHeight) {
            $(target).width(parentWidth);
        }
        else {
            $(target).height(parentHeight);
        }

        var cssLeft = parseInt((target).css("left"));
        var cssTop = parseInt((target).css("top"));

        if (parentWidth != $(target).width()) {
            $(target).css("left", cssLeft + ((parentWidth - $(target).width()) / 2) + "px");
        }
        if (parentHeight != $(target).height()) {
            $(target).css("top", cssTop + ((parentHeight - $(target).height()) / 2) + "px");
        }
    });

    // 上
    $("a:has(.ui-icon-triangle-1-n)").click(function(e) {
        var rel = $(this).attr("rel");
        var target = $(".preview_image", getPhoto(rel));
        var height = $(target).height();
        var h = height / 100;
        var cssTop = parseInt((target).css("top"));
        $(target).css("top", (cssTop - (!_longStep ? h * 10 : h)) + "px");
    });
    // 下
    $("a:has(.ui-icon-triangle-1-s)").click(function(e) {
        var rel = $(this).attr("rel");
        var target = $(".preview_image", getPhoto(rel));
        var height = $(target).height();
        var h = height / 100;
        var cssTop = parseInt((target).css("top"));
        $(target).css("top", (cssTop + (!_longStep ? h * 10 : h)) + "px");
    });
    // 左
    $("a:has(.ui-icon-triangle-1-w)").click(function(e) {
        var rel = $(this).attr("rel");
        var target = $(".preview_image", getPhoto(rel));
        var width = $(target).width();
        var w = width / 100;
        var cssLeft = parseInt((target).css("left"));
        $(target).css("left", (cssLeft - (!_longStep ? w * 10 : w)) + "px");
    });
    // 右
    $("a:has(.ui-icon-triangle-1-e)").click(function(e) {
        var rel = $(this).attr("rel");
        var target = $(".preview_image", getPhoto(rel));
        var width = $(target).width();
        var w = width / 100;
        var cssLeft = parseInt((target).css("left"));
        $(target).css("left", (cssLeft + (!_longStep ? w * 10 : w)) + "px");
    });

    // 左上
    $("a:has(.ui-icon-triangle-1-nw)").click(function(e) {
        var rel = $(this).attr("rel");
        $("a:has(.ui-icon-triangle-1-n)[rel=" + rel + "]").trigger("click");
        $("a:has(.ui-icon-triangle-1-w)[rel=" + rel + "]").trigger("click");
    });
    // 右上
    $("a:has(.ui-icon-triangle-1-ne)").click(function(e) {
        var rel = $(this).attr("rel");
        $("a:has(.ui-icon-triangle-1-n)[rel=" + rel + "]").trigger("click");
        $("a:has(.ui-icon-triangle-1-e)[rel=" + rel + "]").trigger("click");
    });
    // 左下
    $("a:has(.ui-icon-triangle-1-sw)").click(function(e) {
        var rel = $(this).attr("rel");
        $("a:has(.ui-icon-triangle-1-s)[rel=" + rel + "]").trigger("click");
        $("a:has(.ui-icon-triangle-1-w)[rel=" + rel + "]").trigger("click");
    });
    // 右下
    $("a:has(.ui-icon-triangle-1-se)").click(function(e) {
        var rel = $(this).attr("rel");
        $("a:has(.ui-icon-triangle-1-s)[rel=" + rel + "]").trigger("click");
        $("a:has(.ui-icon-triangle-1-e)[rel=" + rel + "]").trigger("click");
    });

    // 打印
    $("#print").click(function() {
        var index = $("#tabs").tabs("option", "selected") + 1;
        document.getElementById("frm" + index).contentWindow.focus();
        document.getElementById("frm" + index).contentWindow.print();
    });

    $("fieldset")
    .mouseover(function() {
        $("legend", $(this)).css("color", "red");
        var rel = $("legend", $(this)).attr("rel");
        $(getPhoto(rel)).css("border", "solid 1px red");
    })
    .mouseout(function() {
        $("legend", $(this)).css("color", "#000");
        var rel = $("legend", $(this)).attr("rel");
        $(getPhoto(rel)).css("border", "dashed 1px #000");
    });
});

var _longStep = false;

function getPhoto(rel) {
    return $(".photo", document.getElementById("frm" + rel.substring(0, 1)).contentWindow.document)[parseInt(rel.substring(1)) - 1];
}