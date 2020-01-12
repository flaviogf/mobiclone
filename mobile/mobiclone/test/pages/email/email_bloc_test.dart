import 'package:flutter_test/flutter_test.dart';
import 'package:mobiclone/core/sign_up/email_repository.dart';
import 'package:mobiclone/pages/email/email_bloc.dart';
import 'package:mobiclone/pages/email/email_event.dart';
import 'package:mobiclone/pages/email/email_state.dart';
import 'package:mockito/mockito.dart';

class MockEmailRepository extends Mock implements EmailRepository {}

void main() {
  group('EmailBloc should', () {
    MockEmailRepository _emailRepository;
    EmailBloc _bloc;

    setUp(() {
      _emailRepository = MockEmailRepository();
      _bloc = EmailBloc(_emailRepository);
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
          _emailRepository.addEmail('flavio@email.com'),
        ).thenAnswer(
          (_) => Future.value(('flavio@email.com')),
        );

        _bloc.add(EmailEventSubmit('flavio@email.com'));

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
        _bloc.add(EmailEventSubmit(''));

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
