import 'package:flutter_test/flutter_test.dart';
import 'package:mobiclone/core/sign_up/sign_up_repository.dart';

void main() {
  group('SignUpRepository addEmail should', () {
    SignUpRepository _repository;

    setUp(() {
      _repository = SignUpRepository();
    });

    test(
      'return the name which was added',
      () async {
        final String name = await _repository.addName('flavio');

        expect(name, 'flavio');
      },
    );
  });

  group('SignUpRepository addEmail should', () {
    SignUpRepository _repository;

    setUp(() {
      _repository = SignUpRepository();
    });

    test(
      'return the email which was added',
      () async {
        final String email = await _repository.addEmail('flavio@email.com');

        expect(email, 'flavio@email.com');
      },
    );
  });

  group('SignUpRepository addPassword should', () {
    SignUpRepository _repository;

    setUp(() {
      _repository = SignUpRepository();
    });

    test(
      'return the password which was added',
      () async {
        final String password = await _repository.addPassword('test123');

        expect(password, 'test123');
      },
    );
  });
}
