const presentationTypes = [
    { Id: 1, Name: "Workshop" },
    { Id: 2, Name: "Keynote" },
    { Id: 3, Name: "Poster" },
    { Id: 4, Name: "Other" }
];

document.addEventListener("DOMContentLoaded", function () {
    const selectHeader = document.getElementById("select-header");
    const selectOptions = document.getElementById("select");
    const sessionCount = document.getElementById("sessionCount");
    const sessionsDiv = document.getElementById("sessions");

    selectHeader.addEventListener("click", function (event) {
        selectOptions.style.display = selectOptions.style.display === "block" ? "none" : "block";
        event.stopPropagation();
    });

    document.addEventListener("click", function (event) {
        selectOptions.style.display = "none";
    });

    selectOptions.addEventListener("click", function (event) {
        event.stopPropagation();
    });

    sessionCount.addEventListener("change", function () {
        const selectedSessionCount = parseInt(sessionCount.value);
        sessionsDiv.innerHTML = "";

        for (let i = 1; i <= selectedSessionCount; i++) {
            const sessionDiv = document.createElement("div");
            sessionDiv.classList.add("session-group");
            sessionDiv.innerHTML = `
          <h3 class="session-header">Session ${i}</h3>
  
          <div class="input-box">
            <input type="text" class="input-field" placeholder="Session Title" required>
          </div>
  
          <div class="input-box">
            <input type="text" class="input-field" placeholder="Session Topic" required>
          </div>

          <div class="input-box">
                <label for="presentationType${i}" class="label">Presentation Type</label>
                <select class="input-field" id="presentationType${i}">
                <option value="" disabled selected>Select a presentation type</option>
                ${presentationTypes.map(type => `<option value="${type.Id}">${type.Name}</option>`).join('')}
                </select>
        </div>
  
          <div class="input-box">
            <label for="presenter${i}" class="label">Presenter</label>
            <select class="input-field" id="presenter${i}">
            <option value="" disabled selected>Select a presenter</option>
            ${allPresenters.map(presenter => `<option value="${presenter.Id}">${presenter.FirstName} ${presenter.LastName}</option>`).join('')}
            </select>
          </div>
  
          <div class="input-box">
            <label for="startTime${i}" class="label">Start Time</label>
            <input type="datetime-local" id="startTime${i}" class="input-field" required>
          </div>
  
          <div class="input-box">
            <label for="endTime${i}" class="label">End Time</label>
            <input type="datetime-local" id="endTime${i}" class="input-field">
          </div>
        `;
            sessionsDiv.appendChild(sessionDiv);
        }
    });


    document.getElementById("submit").addEventListener("click", function (event) {
        event.preventDefault();

        let sessions = [];

        const selectedSessionCount = parseInt(sessionCount.value);

        for (let i = 1; i <= selectedSessionCount; i++) {
            let session = {
                title: document.getElementById(`sessionTitle${i}`).value,
                topic: document.getElementById(`sessionTopic${i}`).value,
                presentationType: document.getElementById(`presentationType${i}`).value,
                presenter: document.getElementById(`presenter${i}`).value,
                startTime: document.getElementById(`startTime${i}`).value,
                endTime: document.getElementById(`endTime${i}`).value
            };

            sessions.push(session);
        }

        document.getElementById("sessionsJson").value = JSON.stringify(sessions);

        addSelectedReviewers();

        let form = document.querySelector("form");
        form.submit();
    });

    // Event listener for checkboxes to add selected reviewers
    document.querySelectorAll("#select input[type=checkbox]").forEach(function (checkbox) {
        checkbox.addEventListener("change", addSelectedReviewers);
    });

    function addSelectedReviewers() {
        let selectedReviewers = [];
        let checkboxes = document.querySelectorAll("#select input[type=checkbox]:checked");
        checkboxes.forEach(function (checkbox) {
            selectedReviewers.push(checkbox.id.split("_")[1]);
        });

        let hiddenInput = document.querySelector("input[name='SelectedReviewers']");
        if (!hiddenInput) {
            hiddenInput = document.createElement("input");
            hiddenInput.setAttribute("type", "hidden");
            hiddenInput.setAttribute("name", "SelectedReviewers");
            hiddenInput.setAttribute("value", "");
            let form = document.querySelector("form");
            form.appendChild(hiddenInput);
        }
        hiddenInput.value = selectedReviewers.join(",");
    }


});
