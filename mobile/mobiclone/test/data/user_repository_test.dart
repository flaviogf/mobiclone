import 'package:flutter_test/flutter_test.dart';
import 'package:mobiclone/data/user_repository.dart';
import 'package:mockito/mockito.dart';
import 'package:shared_preferences/shared_preferences.dart';

class MockSharedPreferences extends Mock implements SharedPreferences {}

void main() {
  group('UserRepository addName should', () {
    MockSharedPreferences _sharedPreferences;
    UserRepository _repository;

    setUp(() {
      _sharedPreferences = MockSharedPreferences();
      _repository = UserRepository(_sharedPreferences);
    });

    test(
      'return the name which was added',
      () async {
        final String name = await _repository.addName('flavio');

        expect(name, 'flavio');
      },
    );

    test(
      'save the name which was added',
      () async {
        await _repository.addName('flavio');

        verify(_sharedPreferences.setString('@name', 'flavio'));
      },
    );
  });

  group('UserRepository addEmail should', () {
    MockSharedPreferences _sharedPreferences;
    UserRepository _repository;

    setUp(() {
      _sharedPreferences = MockSharedPreferences();
      _repository = UserRepository(_sharedPreferences);
    });

    test(
      'return the email which was added',
      () async {
        final String email = await _repository.addEmail('flavio@email.com');

        expect(email, 'flavio@email.com');
      },
    );

    test(
      'save the email which was added',
      () async {
        await _repository.addEmail('flavio@email.com');

        verify(_sharedPreferences.setString('@email', 'flavio@email.com'));
      },
    );
  });

  group('UserRepository addPassword should', () {
    MockSharedPreferences _sharedPreferences;
    UserRepository _repository;

    setUp(() {
      _sharedPreferences = MockSharedPreferences();
      _repository = UserRepository(_sharedPreferences);
    });

    test(
      'return the password which was added',
      () async {
        final String password = await _repository.addPassword('test123');

        expect(password, 'test123');
      },
    );

    test('save the password which was added', () async {
      await _repository.addPassword('test123');

      verify(_sharedPreferences.setString('@password', 'test123'));
    });
  });
}
