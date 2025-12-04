const namePlace = document.querySelector(".namePlace")
const name = JSON.parse(sessionStorage.getItem("user"))
const f_name=name.userFirstName
namePlace.textContent = `ברוך הבא ${f_name} מייד נצלול פנימה`

//update
async function updateDetails() {
    const storedUser = JSON.parse(sessionStorage.getItem("user"));
    const id = storedUser.userId;
    const userName = document.querySelector("#userName").value
    const firstName = document.querySelector("#firstName").value
    const lastName = document.querySelector("#lastName").value
    const password = document.querySelector("#password").value
    const updateUser = {
        UserId: id,
        UserName: userName,
        UserFirstName: firstName,
        UserLastName: lastName,
        UserPassword: password
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
            alert("העדכון נכשל! כנראה הזנת סיסמא חלשה מידי...")
            throw new Error(`HTTP error! status ${response.status}`);
        }
        else {
            alert("המשתמש עודכן בהצלחה")
            const updatedUser = await response.json();
            sessionStorage.setItem("user", JSON.stringify(updatedUser));
        }
    }
    catch (e) { alert(e) }
}