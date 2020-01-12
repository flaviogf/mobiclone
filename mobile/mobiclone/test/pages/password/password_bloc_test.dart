import 'package:flutter_test/flutter_test.dart';
import 'package:mobiclone/core/sign_up/password_repository.dart';
import 'package:mobiclone/pages/password/password_bloc.dart';
import 'package:mobiclone/pages/password/password_event.dart';
import 'package:mobiclone/pages/password/password_state.dart';
import 'package:mockito/mockito.dart';

class MockPasswordRepository extends Mock implements PasswordRepository {}

void main() {
  group('PasswordBloc should', () {
    MockPasswordRepository _passwordRepository;
    PasswordBloc _bloc;

    setUp(() {
      _passwordRepository = MockPasswordRepository();
      _bloc = PasswordBloc(_passwordRepository);
    });

    tearDown(() {
      _bloc.close();
    });

    test(
      'have the initial state equal to "InitiatedPasswordState"',
      () {
        expect(_bloc.initialState, InitiatedPasswordState());
      },
    );

    test(
      'return "ValidatedPasswordState" when the password submitted is valid',
      () {
        when(
          _passwordRepository.addPassword('test123'),
        ).thenAnswer(
          (_) => Future.value('test123'),
        );

        _bloc.add(PasswordEventSubmit('test123'));

        expectLater(
          _bloc,
          emitsInOrder([
            InitiatedPasswordState(),
            SubmittedPasswordState('test123'),
            ValidatedPasswordState('test123'),
          ]),
        );
      },
    );

    test(
      'return "RequiredPasswordState" when the password submitted is empty',
      () {
        _bloc.add(PasswordEventSubmit(''));

        expectLater(
          _bloc,
          emitsInOrder([
            InitiatedPasswordState(),
            SubmittedPasswordState(''),
            RequiredPasswordState('', 'The password must be informed.'),
          ]),
        );
      },
    );
  });
}
