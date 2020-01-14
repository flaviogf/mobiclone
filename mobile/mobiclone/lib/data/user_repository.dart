import 'package:shared_preferences/shared_preferences.dart';

class UserRepository {
  SharedPreferences _preferences;

  UserRepository(this._preferences);

  Future<String> addName(String name) async {
    await _preferences.setString("@name", name);

    return name;
  }

  Future<String> addEmail(String email) async {
    await _preferences.setString("@email", email);

    return email;
  }

  Future<String> addPassword(String password) async {
    await _preferences.setString("@password", password);

    return password;
  }

  Future<String> getName() async {
    final String name = _preferences.getString('@name');

    return name;
  }

  Future<String> getEmail() async {
    final String email = _preferences.getString('@email');

    return email;
  }

  Future<String> getPassword() async {
    final String password = _preferences.getString('@password');

    return password;
  }
}
