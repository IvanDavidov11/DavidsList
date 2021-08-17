const element = {
    btnSpec: () => document.getElementById('specificBtn'),
    specDiv: () => document.getElementById('divSpecific')
}

if (window.location.href.includes('RandomMovieGenerator')) {
    element.btnSpec().addEventListener('click', showMore)
    function showMore(e) {
        if (e.target.innerText == "SOMETHING SPECIFIC") {
            if (element.specDiv().style.display == 'none') {
                element.specDiv().style.display = 'block';
            }
            else {
                element.specDiv().style.display = 'none';
            }
        }
    }
}