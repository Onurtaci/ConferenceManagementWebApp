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
                <label for="sessionTitle${i}" class="label">Title</label>
                <input type="text" id="sessionTitle${i}" class="input-field" required>
                <span asp-validation-for="Title" class="text-danger"></span>
          </div>

          <div class="input-box">
                <label for="sessionTopic${i}" class="label">Topic</label>
                <input type="text" id="sessionTopic${i}" class="input-field" required>
                <span asp-validation-for="Topic" class="text-danger"></span>
          </div>

          <div class="input-box">
                <label for="presentationType${i}" class="label">Presentation Type</label>
                <select class="input-field" id="presentationType${i}">
                <option value="" disabled selected>Select a presentation type</option>
                ${presentationTypes.map(type => `<option value="${type.Id}">${type.Name}</option>`).join('')}
                </select>
                <span asp-validation-for="PresentationType" class="text-danger"></span>
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
            <span asp-validation-for="StartTime" class="text-danger"></span>
          </div>
  
          <div class="input-box">
            <label for="endTime${i}" class="label">End Time</label>
            <input type="datetime-local" id="endTime${i}" class="input-field">
            <span asp-validation-for="EndTime" class="text-danger"></span>
          </div>
        `;
            sessionsDiv.appendChild(sessionDiv);
        }
    });


    document.getElementById("submitButton").addEventListener("click", function (event) {
        event.preventDefault();

        let sessionsData = [];

        for (let i = 1; i <= parseInt(sessionCount.value); i++) {
            const session = {
                Title: getValueById(`sessionTitle${i}`),
                Topic: getValueById(`sessionTopic${i}`),
                PresentationType: parseInt(getValueById(`presentationType${i}`)),
                PresenterId: getValueById(`presenter${i}`),
                StartTime: new Date(getValueById(`startTime${i}`)),
                EndTime: new Date(getValueById(`endTime${i}`))
            };

            sessionsData.push(session);
        }

        let selectedReviewers = [];

        document.querySelectorAll('input[type="checkbox"][name^="reviewer_"]').forEach(function (checkbox) {
            if (checkbox.checked) {
                selectedReviewers.push(checkbox.value); // Add selected reviewer ID to the array
            }
        });

        document.getElementById("sessionsData").value = JSON.stringify(sessionsData);
        document.getElementById("selectedReviewers").value = JSON.stringify(selectedReviewers);
        
        document.getElementById("conferenceForm").submit();
    });

    function getValueById(id) {
        const element = document.getElementById(id);
        return element ? element.value : "";
    }
});
