//mobile search bar
//filter mobile
$('.aggregates-desktop-category-btn').click(function() {
    $('.aggregates-desktop-category-btn').removeClass('active');
    $(this).addClass('active');
    var categoryName = $(this).attr('category');
    scrollToCategory(categoryName);
});
function showAggregateCalculatorModal() {
    $('#full-aggregate-calculator').modal('show');
}
$('#back-to-all-aggregates').click(function() {
    $('.aggregates-main-container').removeClass('search');
})
$('#aggregates-search-btn').click(function (e) {
    $('.aggregates-main-container').addClass('search');
    var searchTerm = $('#aggregates-search-text').val();
    var aggregateSearchItemsContainer = '.aggregates-search-items-container';
    $('#search-result-keyword').html("<b>"+searchTerm+"</b>");
    var searchContentsHtml = "";
    $('.aggregate-wrapper').each(function () {
        var aggregateName = $(this).find('.aggregates-collapse-container > .aggregate-title-over-img > p').html().toLowerCase();
        if (aggregateName.indexOf(searchTerm.toLowerCase()) !== -1) {
            searchContentsHtml += this.outerHTML;
        }
    })
    if (searchContentsHtml.length > 0) {
        searchContentsHtml = "<div class='row row-plr-0'>" + searchContentsHtml + "</div>";
    } else {
        searchContentsHtml = "<div class='no-aggregate-results'><p class='mb-0'><i class='fas fa-times pr-2'></i>There are no aggregates found with this search term.</p></div>"
    }
    $(aggregateSearchItemsContainer).html(searchContentsHtml);
    e.preventDefault();
})