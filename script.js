document.getElementById("registrationForm").addEventListener("submit", function (event) {
    event.preventDefault();

    let name = document.getElementById("name").value.trim();
    let email = document.getElementById("email").value.trim();
    let course = document.getElementById("course").value;
    let terms = document.getElementById("terms").checked;
    let gender = document.querySelector('input[name="gender"]:checked');

    let errorDiv = document.getElementById("error");
    let resultDiv = document.getElementById("result");

    errorDiv.innerHTML = "";
    resultDiv.innerHTML = "";

    if (name === "") {
        errorDiv.innerHTML = "Name should not be empty";
        return;
    }

    if (email === "") {
        errorDiv.innerHTML = "Please enter a valid email";
        return;
    }

    if (!gender) {
        errorDiv.innerHTML = "Please select gender";
        return;
    }

    if (course === "") {
        errorDiv.innerHTML = "Please select a course";
        return;
    }

    if (!terms) {
        errorDiv.innerHTML = "Please accept Terms & Conditions";
        return;
    }

    resultDiv.innerHTML = `
        <div class="success">Registration Successful!</div>
        <div class="output">
            <p><strong>Name:</strong> ${name}</p>
            <p><strong>Email:</strong> ${email}</p>
            <p><strong>Gender:</strong> ${gender.value}</p>
            <p><strong>Course:</strong> ${course}</p>
        </div>
    `;

    document.getElementById("registrationForm").reset();
});
