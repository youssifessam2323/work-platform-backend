# *APIS* : 

## User API : 

- [GET] The Current Authenticationed User [DONE]

- [GET] Teams of  User in A specific Room [DONE]

- [GET] user by username [DONE]

- [POST] Add a user to the ProjectManagers of a specific Room (Note Must be member in a team of that room) [DONE]

- [PUT] Changes the team leader(to do this operation, the user must be the leader of this team) [DONE] 

- [POST] Join Team by team Code(need the permission logic) [DONE]

- [GET] the User's Teams that lead in a room [DONE]

- [GET] the User's Task's assigned to him At Specific team [DONE]

- [GET] All Rooms By Specific User [DONE]

- [DELETE] Delete A User [UNDONE]

- [PUT] Edit User Data [DONE]

- [MSH 3AREF] Log out [UNDONE]

---


## Room API ; 

- [GET] Room's Settings [UNDONE]

- [POST] Add setting to a room  [UNDONE]

- [POST] Create A room  [DONE]

- [GET] Room By Id [DONE]

- [GET] Teams of a Specific Room [DONE]

- [GET] Room's Project Managers [DONE]


- [GET] Room's Project [DONE]

- [PUT] Update The Room Data [DONE]

- [DELETE] Delete the Room [DONE]


---



## Team API : 

- [POST] Create a new Team in specific room [DONE]

- [GET] A Team by Id [DONE]

- [GET] team's SubTeams [DONE]

- [GET] team's Members [DONE]

- [GET] projects assign to a specific team [DONE]

- [GET] team's tasks [DONE]

- [PUT] Update existing team by Id [DONE]

- [DELETE] Delete existing team by Id [DONE]
---


## Task

- [POST] create new task in a specific team [DONE]

- [POST] create new task in a specific project [DONE]

- [POST] create new Comment in a task [UNDONE]

- [POST] create new Attachment in a task [UNDONE]

- [GET] tasks By Id [DONE]

- [GET] Attachments in a task [DONE]

- [GET] Dependant task/s of that task [DONE]

- [GET] Get task's Checkpoints [DONE]

- [POST]Save task's Checkpoint  [DONE]

- [GET] task's Comments(with their replies) [DONE]

- [GET] task's Sessions of the auth user [DONE]

- [PUT] update Task by Id [DONE]

- [DELETE] delete a task by Id [NOTWORKED]


---

## Project (must be from a project managers in the specific room)

- [POST] create new Project in a room [DONE]

- [POST] add new team to a project [DONE]

- [POST] add new task to a project [DONE]

- [GET] project's tasks [DONE]

- [GET] teams assigned in a project [DONE] 

- [PUT] project by Id [DONE]

- [DELETE] project by ID [NOTWORKED]

- [DELETE] remove a team from a project [DONE]

---

## Checkpoint 

- [POST] create new checkpoint in a specific task [UNDONE]

- [POST] new Checkpoint subtask [UNDONE]

- [GET] a checkpoint by Id [UNDONE] 

- [GET] all checkpoint's subtasks [UNDONE] 

- [PUT] update a checkpoint by Id [UNDONE]

- [DELETE] delete a checkpoint by Id [UNDO]



---
---
---


# Ideas Want to do : 

- Define a dto for each entity in the system (including there nested properties)

---

# Problems 

- The Delete API of the Project don't work(because of the relationship with task)

- The Delete API of the Task don't work

- I want to Define nested Dtos to hide some Critical information

- How to handle Exceptions 



------
------
------
------ 
# Very Important Notes To do in Production : 

- Make the Task's teamId foreign key to be nullabe 