var siteUrl = window.location.pathname;
var splitUrl = siteUrl.split('/');

var companies = document.getElementById("companies");
var taskStatus = document.getElementById("taskStatus");
var categories = document.getElementById("categories");
var ownIndex = document.getElementById("ownIndex");
var taskas = document.getElementById("taskas");
var reg = document.getElementById("reg");

if (splitUrl[2] == "OwnIndex") {
    ownIndex.classList.add("active");
    companies.classList.remove("active");
    taskStatus.classList.remove("active");
    taskas.classList.remove("active");
    categories.classList.remove("active");
    reg.classList.remove("active");
}
else if (splitUrl[1] == "Taskas") {
    taskas.classList.add("active");
    companies.classList.remove("active");
    taskStatus.classList.remove("active");
    categories.classList.remove("active");
    reg.classList.remove("active");
    ownIndex.classList.remove("active");
}
else if (splitUrl[1] == "TaskStatus") {
    taskStatus.classList.add("active");
    companies.classList.remove("active");
    taskas.classList.remove("active");
    categories.classList.remove("active");
    reg.classList.remove("active");
    ownIndex.classList.remove("active");
}
else if (splitUrl[1] == "Companies") {
    companies.classList.add("active");
    taskStatus.classList.remove("active");
    taskas.classList.remove("active");
    categories.classList.remove("active");
    reg.classList.remove("active");
    ownIndex.classList.remove("active");
}
else if (splitUrl[1] == "Categories") {
    categories.classList.add("active");
    companies.classList.remove("active");
    taskStatus.classList.remove("active");
    taskas.classList.remove("active");
    reg.classList.remove("active");
    ownIndex.classList.remove("active");
}
else {
    reg.classList.add("active");
    companies.classList.remove("active");
    taskStatus.classList.remove("active");
    taskas.classList.remove("active");
    categories.classList.remove("active");
    ownIndex.classList.remove("active");
}