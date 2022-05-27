function scrollToElement(selector, duration, offset, highlightElement) {
    var additionalOffset = 0;
    if (offset != null) { additionalOffset = offset }
    var mainOffset = $(selector).offset().top + additionalOffset;
    $('html, body').animate({
        scrollTop: mainOffset
    }, duration);
    //if (highlightElement) {
    //    $(selector).addClass('highlight-element');
    //    setTimeout(function () { $(selector).removeClass('highlight-element'); }, 1000);
    //}
}
$(document).ready(function () {
    $('#nav-icon1,#nav-icon2,#nav-icon3,#nav-icon4').click(function () {
        $(this).toggleClass('open');
    });
});
//scroll to top btn
var btn = $('#scroll-up-btn');
$(window).scroll(function () {
    if ($(window).scrollTop() > 300) {
        btn.fadeIn();
    } else {
        btn.fadeOut();
    }
});
btn.on('click', function (e) {
    e.preventDefault();
    $('html, body').animate({ scrollTop: 0 }, '300');
});
//highlight page nav btn
if (highlightNavButtonNames != null && highlightNavButtonNames != "") {
    if (highlightNavButtonNames.includes(',')) {
        var navButtonNamesArray = highlightNavButtonNames.split(',');
        for (var i = 0; i < highlightNavButtonNames.length; i++) {
            highlightNavBtn(navButtonNamesArray[i]);
        }
    } else {
        highlightNavBtn(highlightNavButtonNames);
    }
}
function highlightNavBtn(btnSelector) {
    $('#' + btnSelector).addClass('nav-active');
}
//enquiry list hover
var enquirylistContents = ".enquiry-list-contents";
var enquiryListLoader = "#enquiry-list-loader";
function toggleEnquiryList() {
    $(enquirylistContents).toggleClass('d-block');
}
function hideEnquiryList() {
    $(enquirylistContents).removeClass('d-block');
}
function showEnquiryList() {
    $(enquirylistContents).addClass('d-block');
    $('.navbar-toggler').addClass('collapsed');
    $('.navbar-collapse').removeClass('show');
}
$(document).on('#enquiry-btn', 'click touchstart',function() {
    toggleEnquiryList();
})
$('.enquiry-list, .enquiry-list-contents').hover(function () {
    showEnquiryList();
})
$(".enquiry-btn").mouseleave(function () {
    hideEnquiryList();
});
$(document).on('mouseleave', '.enquiry-list-contents',function() {
    hideEnquiryList();
})
$(document).on('touchstart click', '.enquiry-list-close-btn', function () {
    hideEnquiryList();
})
function refreshEnquiryList() {
    //setAggregateCookie("soft-sand=12,sharp-sand=18");
    $(enquiryListLoader).show();
    var aggregatesInfo = convertAggregateCookieStringToJson();

    var enquiryListContentHtml = "";
    for (var i = 0; i < aggregatesInfo.length; i++) {
        enquiryListContentHtml += getEnquiryListItemHtml(aggregatesInfo[i].aggregateName, aggregatesInfo[i].quantityUnit, aggregatesInfo[i].quantity);
    }
    $('.enquiry-items-container').html(enquiryListContentHtml);
    var numberOfEnquiryItems = getNumberOfCookiesInEnquiryList(aggregatesInfo);
    $('#enquiry-list-counter').html(numberOfEnquiryItems);
    if (numberOfEnquiryItems>0) {
        $(enquirylistContents).addClass('has-items');
    } else {
        $(enquirylistContents).removeClass('has-items');
    }
    $(enquiryListLoader).hide();
}
function getNumberOfCookiesInEnquiryList(aggregatesInfo) {
    return aggregatesInfo.filter(function (item) { return item.aggregateName }).length;
}
refreshEnquiryList();
function getEnquiryListItemHtml(aggregateName, aggregateUnit, quantity) {
    var formattedAggregateName = aggregateName.replace(/\_/g, " ");
    var formattedAggregateImageName = aggregateName.replace(/\_-_bulk_bag/g, "").replace(/\_/g, "-");
    var formattedQuantityUnit = aggregateUnit.replace("-", " ");
    return `<div class="aggregate-enquiry-list-item-container">
    <picture>
    <img data-src="/images/` + formattedAggregateImageName + `-xs.jpg" alt="`+formattedAggregateName+` Small Aggregate Icon Image" class="enquiry-list-img lazyload"/>
    </picture>
    <span class="enquiry-list-aggregate-name">`+ formattedAggregateName+`</span>
    <div class="enquiry-list-quantity">
        <input class="form-control enquiry-list-quantity-input quantity-input" quantity-unit="`+ aggregateUnit + `" aggregate-name="`+ aggregateName + `" onChange="enquiryListQuantityChange(this)" min="0" value="` + quantity +`" type="number" />
        <span class="enquiry-list-quantity-text">`+ formattedQuantityUnit +`(s)</span>
    </div>
    <span style="cursor:pointer" onclick="removeCookie('`+ aggregateName +`')">
        <i class="float-right pt-3 fa fa-trash pl-4"></i>
    </span>
    </div>
    <hr class="enquiry-list-item-hr"/>`
}
function convertAggregateCookieStringToJson() {
    var aggregateInfo = Cookies.get("aggregates-rjh");
    var returnData = [];
    if (aggregateInfo !== "" && typeof aggregateInfo !== "undefined") {
        var aggregateArray = aggregateInfo.split(",");
        for (var i = 0; i < aggregateArray.length; i++) {
            var aggregateDataSplit = aggregateArray[i].split("=")
            var aggregateNameAndQuantityUnit = aggregateDataSplit[0].split(":");
            returnData.push({ "aggregateName": aggregateNameAndQuantityUnit[0], "quantityUnit": aggregateNameAndQuantityUnit[1], "quantity": aggregateDataSplit[1] });
        }
    }
    return returnData;
}
//enquiry list cookie
var enableCookiesSelector = '#enable-cookies';
var aggregateCookieAcceptedName = 'ac-a';
$('#reject-cookies').click(function () {
    $(enableCookiesSelector).hide();
})
$('#accept-cookies').click(function () {
    updateAggregateCookieQuantity($(enableCookiesSelector).attr('aggregate-name'), $(enableCookiesSelector).attr('quantity'), $(enableCookiesSelector).attr('quantity-unit'), false);
    Cookies.set(aggregateCookieAcceptedName, true, { expires: 365 });
    $(enableCookiesSelector).hide();
})
function addEnquiryBtnClick(btnSelector) {
    var aggregateName = $(btnSelector).attr('aggregate-name');
    var quantity = $(btnSelector).parent().find('.aggregate-quantity').val();
    var quantityUnit = $(btnSelector).attr('quantity-unit')
    if (!cookiesAccepted()) {
        $(enableCookiesSelector).css('display','table').attr('aggregate-name', aggregateName).attr('quantity', quantity).attr('quantity-unit', quantityUnit);
        return false;
    }
    updateAggregateCookieQuantity(aggregateName, quantity, quantityUnit, false);
}
function cookiesAccepted() {
    var cookieAccepted = Cookies.get(aggregateCookieAcceptedName);
    return cookieAccepted != null;
}
function enquiryListQuantityChange(selector) {
    var aggregateName = $(selector).attr('aggregate-name').replace(' ', '-');
    var quantity = $(selector).val();
    var quantityUnit = $(selector).attr('quantity-unit');
    updateAggregateCookieQuantity(aggregateName, quantity, quantityUnit, true);
}
function setAggregateCookie(aggregateCookieString) {
    Cookies.set('aggregates-rjh', aggregateCookieString, { expires: 365 });
}
function removeCookie(aggregateName) {
    var aggregateCookieInfo = convertAggregateCookieStringToJson();
    aggregateCookieInfo = aggregateCookieInfo.filter((item) => item.aggregateName !== aggregateName);
    var aggregateCookieString = convertAggregateJsonToCookieString(aggregateCookieInfo);
    setAggregateCookie(aggregateCookieString);
    refreshEnquiryList();
    $(enquirylistContents).addClass('d-block');
}
function updateAggregateCookieQuantity(aggregateName, newQuantity, quantityUnit, isFullQuantity) {
    //cookie example: aggregates -> soft-sand=12,sharp-sand=18
    var aggregateCookieInfo = convertAggregateCookieStringToJson();
    var aggregateCookieExists = aggregateCookieExistss(aggregateName, aggregateCookieInfo);
    var correctQuantity = isFullQuantity || !aggregateCookieExists ?
        newQuantity :
        parseInt(aggregateCookieInfo.find(i => i.aggregateName === aggregateName)["quantity"]) + parseInt(newQuantity);
    if (correctQuantity == 0) {
        removeCookie(aggregateName);
    } else {
        if (aggregateCookieExists) {
            aggregateCookieInfo.find(i => i.aggregateName === aggregateName)["quantity"] = correctQuantity;
        } else {
            aggregateCookieInfo.push({ "aggregateName": aggregateName, "quantity": correctQuantity, "quantityUnit": quantityUnit });
        }
        setAggregateCookie(convertAggregateJsonToCookieString(aggregateCookieInfo));
    }
    refreshEnquiryList();
    $(enquirylistContents).addClass('d-block');
}
function aggregateCookieExistss(aggregateName, aggregateCookieInfo) {
    return aggregateCookieInfo.find(i => i.aggregateName === aggregateName) !== undefined ? true : false;
}
function convertAggregateJsonToCookieString(aggregateJson) {
    var aggregateJsonString = "";
    for (var i = 0; i < aggregateJson.length; i++) {
        aggregateJsonString += aggregateJson[i]["aggregateName"] + ":" + aggregateJson[i]["quantityUnit"]+ "=" + aggregateJson[i]["quantity"];
        if (i+1 != aggregateJson.length) {
            aggregateJsonString += ",";
        }
    }
    return aggregateJsonString;
}
//mobile search
$('.toggle-small-screen-search').click(function () {
    if ($('#small-screen-search').hasClass('search-show')) {
        $('#small-screen-search').removeClass('search-show');
        $(this).removeClass('search-close');
    } else {
        $('#small-screen-search').addClass('search-show');
        $(this).addClass('search-close');
    }
})