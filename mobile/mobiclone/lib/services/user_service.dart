import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:mobiclone/models/user.dart';

class UserService {
  http.Client _client;

  UserService(this._client);

  Future<User> addUser(User user) async {
    final Map<String, dynamic> values = Map();

    values["name"] = user.name;
    values["email"] = user.email;
    values["password"] = user.password;

    final Map<String, String> headers = Map();

    headers["Content-Type"] = "application/json";

    final http.Response response = await _client.post(
      'https://9ccb70f5.ngrok.io/user',
      headers: headers,
      body: json.encode(values),
    );

    return user;
  }
}
