const namePlace = document.querySelector(".namePlace")
const name = sessionStorage.getItem("firstName")
namePlace.textContent = `ברוך הבא ${name} מייד נצלול פנימה`

//update
async function updateDetails() {
    const id = Number(sessionStorage.getItem("id"))
    const userName = document.querySelector("#userName").value
    const firstName = document.querySelector("#firstName").value
    const lastName = document.querySelector("#lastName").value
    const password = document.querySelector("#password").value
    const updateUser = {
        Id: id,
        UserName: userName,
        FirstName: firstName,
        LastName: lastName,
        Password: password
    }
    try {
        const response = await fetch(
            `/api/Users/${id}`,
            {
                method: `PUT`,
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(updateUser)
            }
        )
        if (!response.ok) {
            throw new Error(`HTTP error! status ${response.status}`);
        }
        else {
            alert("המשתמש עודכן בהצלחה")
            sessionStorage.setItem("id", updateUser.Id)
            sessionStorage.setItem("userName", updateUser.UserName)
            sessionStorage.setItem("firstName", updateUser.FirstName)
            sessionStorage.setItem("lastName", updateUser.LastName)
            sessionStorage.setItem("password", updateUser.Password)
        }
    }
    catch (e) { alert(e) }
}