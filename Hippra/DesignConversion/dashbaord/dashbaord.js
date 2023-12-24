
// // sidenav selection
function openPage(evt, value) {
    var i, nav__listitem;
    nav__listitem = document.getElementsByClassName("nav__listitem");
    for (i = 0; i < nav__listitem.length; i++) {
      nav__listitem[i].className = nav__listitem[i].className.replace(" nav__listitem-active", "");
    }
    document.getElementById(value).style.display = "block";
    evt.currentTarget.className += " nav__listitem-active";
}

// extended sidenav selection
function openPage_extended(evt, value) {
  var i, expanded_nav__listitem;
  expanded_nav__listitem = document.getElementsByClassName("expanded_nav__listitem");
  for (i = 0; i < expanded_nav__listitem.length; i++) {
    expanded_nav__listitem[i].className = expanded_nav__listitem[i].className.replace(" expanded_nav__listitem-active", "");
  }
  document.getElementById(value).style.display = "block";
  evt.currentTarget.className += " expanded_nav__listitem-active";
}

// expand sidenav
function open_nav() {
  document.getElementById("sidenav-expanded").style.width = "260px";
}

// collapse sidenav
function close_sidenav() {
  document.getElementById("sidenav-expanded").style.width = "0";
}


// toggle between hiding and showing the dropdown content
function myFunction() {

  var dropdown_content = document.getElementById("myDropdown");
 
  if (dropdown_content.style.display == "block") {
    dropdown_content.style.display = "none";
  } else {
    dropdown_content.style.display = "block";
  }
}
