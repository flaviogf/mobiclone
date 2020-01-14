import 'package:flutter_test/flutter_test.dart';
import 'package:mobiclone/data/user_repository.dart';
import 'package:mobiclone/pages/email/email_bloc.dart';
import 'package:mobiclone/pages/email/email_event.dart';
import 'package:mobiclone/pages/email/email_state.dart';
import 'package:mockito/mockito.dart';

class MockUserRepository extends Mock implements UserRepository {}

void main() {
  group('EmailBloc should', () {
    MockUserRepository _userRepository;
    EmailBloc _bloc;

    setUp(() {
      _userRepository = MockUserRepository();
      _bloc = EmailBloc(_userRepository);
    });

    tearDown(() {
      _bloc.close();
    });

    test('have the initial state equal to InitiatedEmailState', () {
      expect(_bloc.initialState, InitiatedEmailState());
    });

    test(
      'return "ValidateEmailState" when the email submitted is valid',
      () {
        when(
          _userRepository.addEmail('flavio@email.com'),
        ).thenAnswer(
          (_) => Future.value(('flavio@email.com')),
        );

        _bloc.add(SubmitEmailEvent('flavio@email.com'));

        expect(
          _bloc,
          emitsInOrder([
            InitiatedEmailState(),
            SubmittedEmailState('flavio@email.com'),
            ValidatedEmailState('flavio@email.com'),
          ]),
        );
      },
    );

    test(
      'return "RequiredEmailState" when the email submitted is empty',
      () {
        _bloc.add(SubmitEmailEvent(''));

        expect(
          _bloc,
          emitsInOrder([
            InitiatedEmailState(),
            SubmittedEmailState(''),
            RequiredEmailState('', 'The email must be informed.'),
          ]),
        );
      },
    );
  });
}
