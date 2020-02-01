import 'dart:convert';

import 'package:http/http.dart' as http;

class SessionService {
  http.Client _client;

  SessionService(this._client);

  Future<String> signWithEmailAndPassword(String email, String password) async {
    final Map<String, dynamic> values = Map();

    values['email'] = email;
    values['password'] = password;

    final Map<String, String> headers = Map();

    headers['Content-Type'] = 'application/json';

    final http.Response response = await _client.post(
      'http://9ccb70f5.ngrok.io/session',
      headers: headers,
      body: json.encode(values),
    );

    if (response.statusCode != 200) {
      return null;
    }

    final String token = json.decode(response.body)['data'];

    return token;
  }
}
