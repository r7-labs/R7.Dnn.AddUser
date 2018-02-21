# R7.Dnn.AddUser

DNN Platfrom module which allow non-admins to create new users.

## Main features

With *R7.Dnn.AddUser* module you can:

- Allow non-admins to create users. Just place module on the page and add "Edit" permission to the specific role.
- Assign newly created users to the one or more roles (with addition to DNN auto assignment roles).
- Control display name generation with a format string using tokens from DNN display name format string plus more.
- Control username generation with a format string. The "Email as Username" DNN setting is respected.
- Control password length while ensuring that minimum length from `web.config` is respected.
- Control password complexity by limiting variety of special characters used.

Currently usernames are transliterated using Unidecode.NET.
