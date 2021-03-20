# *APIS* : 

## User API : 

- [GET] The Current Authenticationed User [DONE]

- [GET] Teams of  User in A specific Room [DONE]

- [GET] user by username [UNDONE]

- [POST] Add a user to the ProjectManagers of a specific Room (Note Must be member in a team of that room)     [UNDONE]

- [PUT] Changes the team leader(to do this operation, the user must be the leader of this team) [UNDONE] 

- [POST] Join Team by team Code(need the permission logic) [Half-DONE]

- [GET] the User's Teams that lead in a room [UNDONE]

- [GET] All Rooms By Specific User [DONE]

- [DELETE] Delete A User [UNDONE]

- [PUT] Edit User Data [UNDONE]

- [MSH 3AREF] Log out [UNDONE]

---


## Room API ; 

- [POST] Create A room  [UNDONE]

- [POST] Add setting to a room  [UNDONE]

- [GET] Room By Id [UNDONE]

- [GET] Teams of a Specific Room [UNDONE]

- [GET] Room's Project Managers [UNDONE]

- [GET] Room's Settings [UNDONE]

- [GET] Room's Project [UNDONE]

- [GET] Room's Leader [UNDONE]

- [PUT] Update The Room Data [UNDONE]

- [DELETE] Delete the Room [UNDONE]


---



## Team API : 

- [POST] Create a new Team in specific room [UNDONE]

- [GET] A Team by Id [UNDONE]

- [GET] team's SubTeams [UNDONE]

- [GET] team's Members [UNDONE]

- [GET] projects assign to a specific team [UNDONE]

- [GET] team's tasks [UNDONE]

- [PUT] Update existing team by Id [UNDONE]

- [DELETE] Delete existing team by Id [UNDONE]
---


## Task

- [POST] create new task in a specific team [UNDONE]

- [GET] tasks By Id [UNDONE]

- [GET] Attachments in a task [UNDONE]

- [GET] Dependant task/s of that task [UNDONE]

- [GET] task's Checkpoints [UNDONE]

- [GET] the User's Task's assigned to him At Specific team [UNDONE]

- [GET] task's Comments(with their replies) [UNDONE]

- [GET] task's Sessions of the auth user [UNDONE]

- [PUT] update Task by Id [UNDONE]

- [DELETE] delete a task by Id [UNDONE]


---

## Project (must be from a project managers in the specific room)

- [POST] create new Project in a room [UNDONE]

- [POST] add new team to a project [UNDONE]


- [GET] project's tasks [UNDONE]

- [GET] teams assigned in a project [UNDONE] 

- [PUT] project by Id [UNDONE]

- [DELETE] project by ID [UNDONE]

- [DELETE] remove a team from a project [UNDONE]

---

## Checkpoint 

- [POST] create new checkpoint in a specific task [UNDONE]

- [POST] new Checkpoint subtask [UNDONE]

- [GET] a checkpoint by Id [UNDONE] 

- [GET] all checkpoint's subtasks [UNDONE] 

- [PUT] update a checkpoint by Id [UNDONE]

- [DELETE] delete a checkpoint by Id [UNDONE]
