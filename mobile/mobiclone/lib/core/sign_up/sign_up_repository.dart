import 'package:mobiclone/core/sign_up/email_repository.dart';
import 'package:mobiclone/core/sign_up/name_repository.dart';
import 'package:mobiclone/core/sign_up/password_repository.dart';

class SignUpRepository
    implements NameRepository, EmailRepository, PasswordRepository {
  Future<String> addName(String name) async {
    return name;
  }

  @override
  Future<String> addEmail(String email) async {
    return email;
  }

  @override
  Future<String> addPassword(String password) async {
    return password;
  }
}
