
//newUser
async function newUser() {
    const userName = document.querySelector("#userName").value
    const firstName = document.querySelector("#firstName").value
    const lastName = document.querySelector("#lastName").value
    const password = document.querySelector("#password").value
    const User = {
        Id: 0, 
        UserName: userName,
        FirstName: firstName,
        LastName: lastName,
        Password: password
    }
    try {
        const response = await fetch(
            "/api/Users",
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(User)
            }
        );
        if (response.ok) {
            alert("המשתמש נרשם בהצלחה!")
        }
        else {
            throw new Error(`HTTP error! status: ${response.status}`)
        }
    }
    catch (e) { alert(e) } 
}
//logIn
async function logIn() { 
    const userName = document.querySelector("#logInName").value
    const password = document.querySelector("#logInPassword").value
    const ExistUser = {
        Id: 0,
        UserName: userName,
        FirstName: null,
        LastName: null,
        Password: password
    }
    try {
        const response = await fetch(
            "/api/Users/login",
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(ExistUser)
            }
        );
        if (!response.ok) {
            alert("שם משתמש או סיסמא שגויים!")
        }
        else {
            const currentUser = await response.json()
            sessionStorage.setItem("id", currentUser["id"])
            sessionStorage.setItem("userName", currentUser["userName"])
            sessionStorage.setItem("firstName", currentUser["firstName"])
            sessionStorage.setItem("lastName", currentUser["lastName"])
            sessionStorage.setItem("password", currentUser["password"])
            window.location = "update.html"
        }
    }
    catch (e) { alert(e) }  
}


