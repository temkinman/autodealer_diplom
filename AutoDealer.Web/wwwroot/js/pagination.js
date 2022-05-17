document.addEventListener("DOMContentLoaded", ready);

function ready() {
    //let pages = $("[id*='page']");
    console.log('ready');

    const pages = document.querySelectorAll("[id *= 'page']");

    //pages.forEach((item) => item.addEventListener('click', (e) => {
    //    console.log('settt....');
    //    sendFilter(e);
    //}
    //));

    $('.navPage').click(function (e) {
        let r = /\d+/;
        let s = this.id;
        var pageId = s.match(r);

        sendPage(pageId);
    });

    //pages.forEach(el => {
    //    console.log('adding function')
    //    el.click(function (e) {
    //        sendFilter(e);
    //    });
    //})

    const sendPage = (pageId) => {
        console.log('sendPage id: ' + pageId);

        let CompanyId = $('#CompanyId').val();
        let ColorId = $('#ColorId').val();
        let ProduceDateFrom = $('#ProduceDateFrom').val();
        let ProduceDateTo = $('#ProduceDateTo').val();
        let PriceFrom = $('#PriceFrom').val();
        let PriceTo = $('#PriceTo').val();
        let KilometreFrom = $('#KilometreFrom').val();
        let KilometreTo = $('#KilometreTo').val();
        let Abs = $('#abs').val();

        let filter = {};

        filter

        //const chx = e.currentTarget;

        //if(chx.checked) {
        //    userSettings.push(chx.name);
        //}
        //else {
        //    const ind = userSettings.indexOf(chx.name);

        //    if(ind > -1) userSettings.splice(ind, 1);
        //}
    }

    const passBtn = document.getElementById("passBtn");
    const allCheckBoxes = document.querySelectorAll("input[type=checkbox]");
}


function sendFilter(e) {
    e.stopPropagation();
    let r = /\d+/;
    let s = this.id;
    var pageId = s.match(r);
    alert(pageId);

    //var r = /\d+/;
    //var id = $(this).attr('id').match(r);
    //console.log(id);
    //getFilter();
    //alert("stop");
}

function getFilter() {
    const allCheckBoxes = document.querySelectorAll("input[type=checkbox]");
    var filter = {};
    
    allCheckBoxes.forEach(el => {
        if (el.checked) {
            filter[el.id] = true;
        }
    })
    console.log('filter ' + filter)

    //foreach (let i = 0; i < allCheckBoxes.length; i++) {
    //    if (chx.checked)
    //}
}


