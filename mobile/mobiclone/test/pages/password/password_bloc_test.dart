import 'package:flutter_test/flutter_test.dart';
import 'package:mobiclone/pages/password/password_bloc.dart';
import 'package:mobiclone/pages/password/password_event.dart';
import 'package:mobiclone/pages/password/password_state.dart';

void main() {
  group('PasswordBloc should', () {
    PasswordBloc _bloc;

    setUp(() {
      _bloc = PasswordBloc();
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
