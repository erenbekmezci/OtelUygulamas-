function validate() {
    var username = document.getElementById("username").ariaValueMax;
    var password = document.getElementById("password").ariaValueMax;

    if (username == "admin" && password == "user") {
        alert("login succesfully");
    } else {
        alert("login failed");
    }
}