import 'package:flutter_test/flutter_test.dart';
import 'package:mobiclone/data/user_repository.dart';
import 'package:mobiclone/pages/password/password_bloc.dart';
import 'package:mobiclone/pages/password/password_event.dart';
import 'package:mobiclone/pages/password/password_state.dart';
import 'package:mobiclone/services/user_service.dart';
import 'package:mockito/mockito.dart';

class MockUserRepository extends Mock implements UserRepository {}

class MockUserService extends Mock implements UserService {}

void main() {
  group('PasswordBloc should', () {
    MockUserRepository userRepository;
    MockUserService _userService;
    PasswordBloc _bloc;

    setUp(() {
      userRepository = MockUserRepository();
      _userService = MockUserService();
      _bloc = PasswordBloc(userRepository, _userService);
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
          userRepository.addPassword('test123'),
        ).thenAnswer(
          (_) => Future.value('test123'),
        );

        _bloc.add(SubmitPasswordEvent('test123'));

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
        _bloc.add(SubmitPasswordEvent(''));

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
