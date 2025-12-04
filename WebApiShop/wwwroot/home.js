
//newUser
async function newUser() {
    const userName = document.querySelector("#userName").value
    const firstName = document.querySelector("#firstName").value
    const lastName = document.querySelector("#lastName").value
    const password = document.querySelector("#password").value
    const User = {
        UserId: 0, 
        UserName: userName,
        UserFirstName: firstName,
        UserLastName: lastName,
        UserPassword: password
    }
    
    try {
        console.log(User)
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
            alert("הרישום נכשל! מומלץ לבדוק חוזק סיסמא")
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
        UserId: 0,
        UserName: userName,
        UserFirstName: " ",
        UserLastName: " ",
        UserPassword: password
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
            sessionStorage.setItem("user", JSON.stringify(currentUser));
            window.location = "update.html"
        }
    }
    catch (e) { alert(e) }  
}

async function CheckPassword() {
    const points = document.querySelectorAll(".point")
    const password = document.querySelector("#password").value
    const UserPassword = {
        Password: password
    }
    for (let i = 0; i < points.length; i++) {
        points[i].style.backgroundColor = "#ddd";
    }
    try {
        
        const response = await fetch(
            "/api/UsersPassword",
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(UserPassword)
            }
        );
        const num = await response.json()
        const count = Math.min(num, points.length);
        for (let i = 0; i < count; i++) {
            points[i].style.backgroundColor = "red";
        }
        if (response.ok) {
            alert(`סיסמא חזקה דרגה:${count}`) 
        }
        else {
            alert(`סיסמא חלשה דרגה:${count}`) 
            throw new Error(`HTTP error! status: ${response.status}`)
        }
    }
    catch (e) { alert(e) }
}


