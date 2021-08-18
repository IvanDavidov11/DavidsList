const sidebarEle = {
    sidebar: () => document.getElementById('sidebar'),
    accDetails: () => document.getElementById('aDets'),
    genrePref: () => document.getElementById('genPref'),
    myProf: () => document.getElementById('myProf'),
}

if (window.location.href.includes('MyProfile')) {
    if (localStorage.getItem("currentSelect") == 'aDetails') {
        sidebarEle.accDetails().classList.add('active');
        sidebarEle.genrePref().classList.remove('active');
    }
    else if (localStorage.getItem("currentSelect") == 'genrePref') {
        sidebarEle.accDetails().classList.remove('active');
        sidebarEle.genrePref().classList.add('active');
    }
    sidebarEle.sidebar().addEventListener('click', navChange)
    function navChange(e) {
        if (e.target.innerText == 'Account Details') {
            localStorage.setItem("currentSelect", "aDetails");
        }
        else if (e.target.innerText == 'Preferences') {
            localStorage.setItem("currentSelect", "genrePref");
        }
    }
}

sidebarEle.myProf().addEventListener('click', defChange)
function defChange(e) {
    if (e.target.innerText == "My Profile") {
        localStorage.setItem("currentSelect", "aDetails");
    }
}
